using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAt : MonoBehaviour
{
    private float rot;
    private float rotSpeed;
    private Vector3 rooot;

    private void Start()
    {
        rotSpeed = 180f;
    }

    private void Update()
    {
        rot += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        rooot.Set(90f,Mathf.Clamp(rot,-45,45),0f);
        //Mathf.Clamp(rot, -45f, 45f);
        transform.localEulerAngles = rooot;
    }
}
