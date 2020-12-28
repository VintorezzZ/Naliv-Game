using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100;
    [SerializeField] private  GameObject controlledWater;
    private WaterController _waterController;
    public bool canRotate;

    private void Start()
    {
        _waterController = controlledWater.GetComponent<WaterController>();
    }

    private void OnMouseDrag()
    {
        if (canRotate)
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            transform.Rotate(Vector3.up, -rotX);
            ClampValveAngle();

            float waterStrength = transform.rotation.y;
            
            _waterController.ChangeStrengthWaterFlow(waterStrength);
        }
    }
    
    private void ClampValveAngle()
    {
        if (transform.rotation.y > 0.980f)
        {
            transform.rotation = Quaternion.Euler(0, 0.980f, 0);
        }
        else if (transform.rotation.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
