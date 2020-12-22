using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAMController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
  
    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            MoveCamera(); 
        }
    }

    private void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, mouseX, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
