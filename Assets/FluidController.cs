using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidController : MonoBehaviour
{
    [SerializeField] private ParticleSystem water1;
    [SerializeField] private ParticleSystem water2;
    private Material fluidMaterial;
    private float water1FlowSize;
    private float water2FlowSize;
    private float generalWaterFlowSpeed;
    private float WaterVolume1Percent;
    private float WaterVolume2Percent;
    private float generalWaterVolumePercent;
    private float WaterRatio;
    private float WaterRationClamped;
    private float blueChannelDelta;
    private float newBlueChannel;
    private float newBlueChannelClamped;


    void Start()
    {
        gameObject.transform.localScale = new Vector3(1, 0, 1);
        fluidMaterial = GetComponent<Renderer>().material;
        fluidMaterial.color = new Color(0, 0.75f, 0.5f, 0.6f);
    }

    void Update()
    {
        CalculateWaterFlowSize();
        
        generalWaterFlowSpeed = (water1FlowSize + water2FlowSize);
        
        if(generalWaterFlowSpeed == 0)
            return;
        
        CalculateWaterVolumesPercent();
        ChangeFluidScale();
        ChangeColor();
    }

    private void ChangeColor()
    {
        // average blue channel = (255 + 0) / 2 = 128 =>
        // => water1 volume == water2 volume
        // water1 volume > water2 volume => 128 + volume1/volume2
        
        // имеем соотношение обоих объемов. Нужно их сравнить.
        // if 1 = 2, то 50/50 = 1 
        // if 1 > 2, то 60/40 = 1.5; 70/30 = 2.3; 80/20 = 4; 90/10 = 9; 
        // if 2 < 1, то 60/40 = 1.5
         
        // blueChannel1 = WaterColor1.b; // 255 blue
        // blueChannel2 = WaterColor2.b; // 0 green

        if (WaterVolume1Percent > WaterVolume2Percent)
        {
            if (WaterVolume2Percent == 0)
                WaterVolume2Percent = 0.001f;
            
            WaterRatio = WaterVolume1Percent / WaterVolume2Percent * 1;
        }
        else
        {
            if (WaterVolume1Percent == 0)
                WaterVolume1Percent = 0.001f;
            
            WaterRatio = WaterVolume2Percent / WaterVolume1Percent * -1;
        }

        WaterRationClamped = Mathf.Clamp(WaterRatio, -10, 10);
        
        blueChannelDelta = 0.5f * WaterRatio / 10;
        newBlueChannel = 0.5f + blueChannelDelta;
        newBlueChannelClamped = Mathf.Clamp(newBlueChannel, 0, 1);

        //fluidMaterialBlueChannel = newBlueChannelClamped;
        fluidMaterial.color = new Color(0, 0.75f, newBlueChannelClamped, 0.6f);
    }

    private void ChangeFluidScale()
    {
        gameObject.transform.localScale += new Vector3(0, generalWaterFlowSpeed * Time.deltaTime, 0);
    }

    private void CalculateWaterVolumesPercent()
    {
        WaterVolume1Percent += water1FlowSize * Time.deltaTime * 100;
        WaterVolume2Percent += water2FlowSize * Time.deltaTime * 100;
        generalWaterVolumePercent = WaterVolume1Percent + WaterVolume2Percent;
    }

    private void CalculateWaterFlowSize()
    {
        water1FlowSize = water1.main.startSize.constant * 0.01f;
        water2FlowSize = water2.main.startSize.constant * 0.01f;
    }
}
