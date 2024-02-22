using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Slide : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private MovementSystem pm;
    public Camera playerCam; // Reference to the PlayerCam

    [Header("Slide")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimeCount;

    public float slideYScale;
    private float startYScale;

    public float slideFOV = 90f; // Field of view when climbing
    public float defaultFOV = 60f; // Default field of view when not climbing

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private bool sliding;

    // [Header("Enemy")]
    // public LayerMask enemyLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<MovementSystem>();

        startYScale = playerObj.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // slide input
        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
            StartSliding();

        // slide cancel
        if (Input.GetKeyUp(slideKey) && pm.sliding)
            StopSliding();
    }

    private void FixedUpdate()
    {
        if (pm.sliding)
            SlidingMovement();
    }

    private void StartSliding()
    {
        pm.sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimeCount = maxSlideTime;

        playerCam.fieldOfView = slideFOV; // fov
    }

    private void SlidingMovement()
    {
        // slide direction
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // sliding on flat
        if(!pm.OnSlope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(inputDir.normalized * slideForce, ForceMode.Force);

            slideTimeCount -= Time.deltaTime;
        }

        // sloped sliding
        else
        {
            rb.AddForce(pm.GetMovementSlopeDir(inputDir) * slideForce, ForceMode.Force);
            rb.AddForce(Vector3.down * 100f, ForceMode.Force);
        }

        if (slideTimeCount <= 0)
            StopSliding();
    }

    private void StopSliding()
    {
        pm.sliding = false;

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);

        playerCam.fieldOfView = defaultFOV; // fov reset
    }
}
