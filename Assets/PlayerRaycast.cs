using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    private Camera _camera;
    public LayerMask valve = 7;

    [SerializeField] private GameObject tablet;
    
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            Debug.DrawRay(ray.origin, _camera.transform.forward*2, Color.red);
            if (Physics.Raycast(ray, out hit, 2f, valve))
            {
                //print(hit.collider.name);
                Rotator rotator = hit.collider.GetComponent<Rotator>();
                rotator.RotateValve(ray);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
            tablet.SetActive(true);

    }

    
   
}
