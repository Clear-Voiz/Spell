using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody Rig;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //|| Input.GetKeyDown(KeyCode.W)
        {
            Rig.AddForce(0f,5f,0f,ForceMode.Impulse);
        }
    }
}
