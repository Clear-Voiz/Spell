using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundS : Spell
{
    private void Awake()
    {
        cooldown = 1f;
        //VFX = Resources.Load("Terra") as GameObject;
        ImpactVFX = null;
        lifespan = 3f;
        speed = 0f;
        PM = 0.6f; //Power Multiplier
        cost = 2f;
        Element = Elements.Earth;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        StartCoroutine(DestroyAfter(lifespan));
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
                    ServerManager.Despawn(other.gameObject);
                    if (spell.Element != Elements.Thunder)
                    {
                        ServerManager.Despawn(gameObject);
                    }
                }
                else
                {
                    ServerManager.Despawn(gameObject);
                }
            }
        }
    }
    
}
