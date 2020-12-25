using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletController : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private GameObject miniMap;
    private Vector3 miniMapHorizontalScale = new Vector3(0.27f, 0.19f, 0.01f);
    private Vector3 miniMapVerticalScale = new Vector3(0.19f, 0.27f, 0.01f);

    private void Start()
    {
        miniMap = GameObject.Find("MiniMap");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) | Input.GetKeyDown(KeyCode.E))
            RotateTablet();
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }
    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

    void RotateTablet()
    {
        if (gameObject.transform.localRotation == Quaternion.Euler(0, 0, 0))
        {
            gameObject.transform.localRotation = Quaternion.Euler(0,0,90);
            miniMapHorizontal();
        }
        else
        {
            gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
            miniMapVertical();
        }
    }

    void miniMapHorizontal()
    {
        miniMap.transform.localRotation = Quaternion.Euler(0, 180, 90);
        miniMap.transform.localScale = miniMapHorizontalScale;
    }
    void miniMapVertical()
    {
        miniMap.transform.localRotation = Quaternion.Euler(0, 180, 0);
        miniMap.transform.localScale = miniMapVerticalScale;
    }
}
