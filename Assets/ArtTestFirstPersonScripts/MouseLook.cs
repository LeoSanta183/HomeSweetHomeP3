using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;

    private bool isPaused = false;

    private CursorLockMode previousLockMode;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        previousLockMode = Cursor.lockState;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        else
        {
            if (isPaused)
            {
                isPaused = false;
                Cursor.lockState = previousLockMode;
            }
        }


        if (!isPaused)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }



    }
}
