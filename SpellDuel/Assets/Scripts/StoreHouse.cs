using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreHouse_",menuName = "ScriptableObjects/Storage")]
public class StoreHouse : ScriptableObject
{
    public float groundLevel;
    public GameObject sparks;
    public GameObject shield;
    public GameObject terra;
    public GameObject fireBall;
    public GameObject thunder;
    public GameObject mgkHit;
    public GameObject impulse;
    public GameObject doom;
    public GameObject paralysis;
    public GameObject ice;
    
    public GameObject LP;

    private void OnEnable()
    {
        if (sparks == null)
        {sparks = Resources.Load("PS_sparks") as GameObject;}

        if (shield == null)
        {shield = Resources.Load("Shield") as GameObject;}

        if (terra == null)
        {terra = Resources.Load("Terra") as GameObject;}

        if (fireBall == null)
        {fireBall = Resources.Load("PS_FireBall") as GameObject;}

        if (thunder == null)
        {thunder = Resources.Load("Thunder") as GameObject;}

        if (mgkHit == null)
        {mgkHit = Resources.Load("Hit") as GameObject;}

        if (impulse == null)
        {impulse = Resources.Load("Impulse") as GameObject;}

        if (doom == null)
        {doom = Resources.Load("Doom") as GameObject;}

        if (LP == null)
        {LP = Resources.Load("LocalPoints") as GameObject;}
        
        if (paralysis == null)
        {paralysis = Resources.Load("Paralysis") as GameObject;}
        
        if (ice == null)
        {ice = Resources.Load("IceShard") as GameObject;}

    }
    //assets

    //TryGetComponent
    //Physics.RaycastAll
}
