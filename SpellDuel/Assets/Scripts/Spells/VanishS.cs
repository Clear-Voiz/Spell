using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishS : Spell,IEffectable
{
    private Material curMat;
    private Material mat;
    private MeshRenderer meshRend;
    private Material inv;
    private float time;
    private Timers tim;
    private void Awake()
    {
        if (GameObject.Find("Player1_mesh").GetComponent<MeshRenderer>().material != null) //TryGetComponent(out curMat)
        {
            meshRend = GameObject.Find("Player1_mesh").GetComponent<MeshRenderer>();
            Debug.Log("found");
        }
        inv = Resources.Load("DissolveMat") as Material;
    }

    private void Start()
    {
        mat = meshRend.material;
        meshRend.material = inv;
        tim = new Timers(3);
    }

    private void Update()
    {
        Effect();
    }

    public void Effect()
    {
        tim.alarm[0] = tim.Chronometer(1f,tim.alarm[0], Disappear);
        
        if (tim.alarm[0] == 1f)
        {
            tim.alarm[1] = tim.Timer(3f,tim.alarm[1]);
        }

        if (tim.alarm[1] == 3f)
        {
            tim.alarm[2] = tim.Chronometer(1f,tim.alarm[2], Reappear);
        }
    }

    public void Reappear()
    {
        var temp = 1f - tim.alarm[2];
        meshRend.material.SetFloat("Time_", temp);
        if (tim.alarm[2] == 1f)
        {
            meshRend.material = mat;
            Destroy(this);
        }
    }

    private void Disappear()
    {
        float disValue = tim.alarm[0];
        meshRend.material.SetFloat("Time_",disValue);
        if (tim.alarm[0] == 1f)
        {
            meshRend.material.SetFloat("Time_",1f);
        }
    }

}
