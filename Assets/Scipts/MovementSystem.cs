using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovementSystem : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;

    private float desMS; // desired moveSpeed
    private float lastDesMS;

    public float speedIncreaseMul;
    public float slopeIncreaseMul;

    public float groundDrag; // drag system to prevent a slipping effect when moving

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCD;
    public float airMult;
    bool jumpReady;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    // [Header("UI Text")]
    // public TextMeshProUGUI stateTextUI;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isGround;

    public bool grounded;

    [Header("Slope Set")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        sliding, 
        air
    }

    public bool sliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        jumpReady = true;

        startYScale = transform.localScale.y;

        // stateTextUI.text = "Walking";
    }

    private void Update()
    {
        // checks if on ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        MyInput();
        SpeedLimiter();
        StateHandler();

        // drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // jump input
        if(Input.GetKey(jumpKey) && jumpReady && grounded)
        {
            jumpReady = false;

            Jump();

            Invoke(nameof(JumpReset), jumpCD);
        }

        // crouch input
        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // cancel crouch
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // string newStateText = "";

        // sliding
        if (sliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
                desMS = slideSpeed;

            else
                desMS = sprintSpeed;

            // newStateText = "Sliding";
        }

        // crouch
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desMS = crouchSpeed;

            // newStateText = "Crouching";
        }

        // sprint
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            desMS = sprintSpeed;

            // newStateText = "Sprinting";
        }

        // walk
        else if (grounded)
        {
            state = MovementState.walking;
            desMS = walkSpeed;

            // newStateText = "Walking";
        }

        // air
        else
        {
            state = MovementState.air;

            // newStateText = "Jumping";
        }

        // checks drastic desMS change
        if(Mathf.Abs(desMS - lastDesMS) > 4f)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothLerpMS());
        }
        else
        {
            moveSpeed = desMS;
        }

        lastDesMS = desMS;

        // Update UI text based on movement state
        // UpdateStateText(newStateText);
    }

    // smooth slow decrease in speed from downhill slide to flat ground slide
    private IEnumerator SmoothLerpMS()
    {
        float time = 0;
        float difference = Mathf.Abs(desMS - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desMS, time / difference);

            if (OnSlope())
            {
                float slopeAng = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngIncrease = 1 + (slopeAng / 90f);

                time += Time.deltaTime * speedIncreaseMul * slopeIncreaseMul * slopeAngIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMul;

            yield return null;
        }

        moveSpeed = desMS;
    }

    private void PlayerMove()
    {
        // move direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetMovementSlopeDir(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMult, ForceMode.Force);

        // slope gravity
        rb.useGravity = !OnSlope();
    }

    private void SpeedLimiter()
    {
        // slope speed limiter
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // ground and air speed limiter
        else
        {
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 VelocityLimit = flatVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(VelocityLimit.x, rb.velocity.y, VelocityLimit.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // resets y velocity so jump always same height

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void JumpReset()
    {
        jumpReady = true;

        exitingSlope = false;
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetMovementSlopeDir(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal);
    }

    // private void UpdateStateText(string newState)
    // {
    //     stateTextUI.text = newState;
    // }
}
