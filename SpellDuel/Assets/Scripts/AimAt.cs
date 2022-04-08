using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class AimAt : MonoBehaviour
{ //this is attached to the wand gameobject
    private float rot;
    private float rotSpeed;
    private Vector3 rooot;

    private void Start()
    {
        rotSpeed = 180f;
    }

    private void Update()
    {
        rot += Input.GetAxisRaw("Mouse X") * rotSpeed * Time.deltaTime;
        rot = Mathf.Clamp(rot, -45f, 45f);
        rooot.Set(transform.localRotation.x,rot,transform.localRotation.z);
        //Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.localRotation.x,rot,transform.localRotation.z), Time.deltaTime);
        transform.localEulerAngles = rooot;
    }
}
