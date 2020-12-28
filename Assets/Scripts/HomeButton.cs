using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButton : MonoBehaviour
{
    [SerializeField] private GameObject tablet;
    
    private void OnMouseDown()
    {
        tablet.SetActive(false);
    }
}
