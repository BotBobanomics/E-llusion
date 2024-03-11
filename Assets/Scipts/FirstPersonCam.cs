using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sensX = PlayerPrefs.GetFloat("Sensitivity");
        sensY = PlayerPrefs.GetFloat("Sensitivity");
    }

    private void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Play)
        {
            // mouse input
            float mouseX = Input.GetAxis("Mouse X") * sensX;
            float mouseY = Input.GetAxis("Mouse Y") * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // locks how far the player cam can go, 90 degrees)

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
            // checks right alt input
            if (Input.GetKeyDown(KeyCode.RightAlt))
            {
                ToggleCursorLock();
            }
    }

    void ToggleCursorLock() // chnages cursor state
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void ChangeSensitivity()
    {
        sensX = PlayerPrefs.GetFloat("Sensitivity");
        sensY = PlayerPrefs.GetFloat("Sensitivity");
    }
}
