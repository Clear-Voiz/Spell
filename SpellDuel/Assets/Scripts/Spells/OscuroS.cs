using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscuroS : Spell
{
    private Light[] illumination;
    private Timers tim;
    public AnimationCurve descendCurve;
    public AnimationCurve ascendCurve;
    private void Awake()
    {
        Element = Elements.NonElemental;
        lifespan = 10f;
        _conjure = FindObjectOfType<Conjure>();
        illumination = FindObjectsOfType<Light>();
    }

    private void Start()
    {
        tim.alarm[0] = 0.5f;
        
       
    }

    private void ShutDown()
    {
        foreach (var lighpoint in illumination)
        {
            
        }
    }
}
