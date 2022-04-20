using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum Elements{NonElemental = 0,Fire = 1,Ice = 2,Thunder = 4,Earth = 8,Water = 16,Wind = 32,Light = 64, Dark= 128}

public abstract class Spell
{
    //var definitions
    public GameObject VFX;
    public GameObject ImpactVFX;
    public float lifespan;
    public float speed;
    public float PM; //Power Multiplier
    public float cost;
    public Elements Element;
    public string ActiveCol;
    public string InactiveCol;
    public Transform transform;
    public Transform dir;
    public GameObject LP;
    

    public abstract void Move();
    public abstract void Impact(Collision other);
}
