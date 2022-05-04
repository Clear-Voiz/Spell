using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SVanish : AlterSpell
{
    private Material curMat;
    private Material mat;
    private MeshRenderer meshRend;
    private Material inv;
    private float time;

    public SVanish()
    {
        if (GameObject.Find("Player1_mesh").GetComponent<MeshRenderer>().material != null) //TryGetComponent(out curMat)
        {
            meshRend = GameObject.Find("Player1_mesh").GetComponent<MeshRenderer>();
            mat = meshRend.material;
            inv = Resources.Load("DissolveMat") as Material;
            meshRend.material = inv;
            Debug.Log("found");
        }

        tim = new Timers(3);
        OnStart();
    }
    
    public override void Effect()
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
            EndEffect();
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

    public override void EndEffect()
    {
        meshRend.material = mat;
        OnEnd();
        
    }
}
