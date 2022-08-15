﻿using UnityEngine;

[CreateAssetMenu(fileName = "StoreHouse_",menuName = "ScriptableObjects/Storage")]
public class StoreHouse : ScriptableObject
{

    public float groundLevel;
    public GameObject sparks;
    public ShieldS shield;
    public GroundS terra;
    public FireS fireBall;
    public ThunderS thunder;
    public HitS mgkHit;
    public GameObject impulse;
    public DoomS doom;
    public GameObject paralysis;
    public GameObject ice;
    public CheckS checker;
    
    public GameObject LP;
    
    

    //assets

    //TryGetComponent
    //Physics.RaycastAll
}
