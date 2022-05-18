using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalysisS : Spell,IShootable
{
    private void Awake()
    {
        Element = Elements.Thunder;
        lifespan = 3f;
        speed = 25f;
    }

    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        transform.Translate(speed*Time.deltaTime*Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            new SParalysis(other);
            
        }
    }
}
