using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    private ParticleSystem _water;
    private ParticleSystem.MainModule mainWaterModule;
    void Start()
    {
        _water = GetComponent<ParticleSystem>();
        mainWaterModule = _water.main;
    }

    public void ChangeStrengthWaterFlow(float strength)
    {
        mainWaterModule.startSize = strength;
    }
    
    
}
