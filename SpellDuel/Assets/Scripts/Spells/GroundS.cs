using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundS : Spell
{
    private void Start()
    {
        //VFX = Resources.Load("Terra") as GameObject;
        ImpactVFX = null;
        lifespan = 3f;
        speed = 0f;
        PM = 0.6f; //Power Multiplier
        cost = 2f;
        Element = Elements.Earth;
        ActiveCol = "#ff0000ff";
        InactiveCol = "#800000ff";
        Destroy(gameObject,lifespan);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spell"))
        {
            Spell spell;
            if (other.TryGetComponent(out spell))
            {
                if (spell.Element != Elements.NonElemental)
                {
                    Destroy(other.gameObject);
                    if (spell.Element != Elements.Thunder)
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    
}
