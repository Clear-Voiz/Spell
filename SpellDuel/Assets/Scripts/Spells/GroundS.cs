using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundS : DefensiveSpell
{
    public GroundS(Transform _transform, Transform _dir)
    { 
        VFX = Resources.Load("Terra") as GameObject;
        ImpactVFX = null;
        lifespan = 3f;
        speed = 0f;
        PM = 0.6f; //Power Multiplier
        cost = 2f;
        Element = Elements.Earth;
        ActiveCol = "#ff0000ff";
        InactiveCol = "#800000ff";
        transform = _transform;
        dir = _dir;
    }
}
