using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreHouse_",menuName = "ScriptableObjects/Storage")]
public class StoreHouse : ScriptableObject
{
    public float groundLevel;
    public GameObject Effects;

    private void OnEnable()
    {
        Effects = GameObject.Find("Effects");
    }
    //assets
    

    private void Start()
    {
        groundLevel = 7.5f;
    }
    
    //TryGetComponent
    //Physics.RaycastAll
}
