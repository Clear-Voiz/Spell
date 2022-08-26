using System;
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

    /*private void Update()
    {
        transform.Translate(0f,1f * Time.deltaTime,0f);
    }*/

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

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Debug.Log("touched");

        if (other.GetContact(0).point == Vector3.up)
        {
            Debug.Log("Atatta");
        }
    }
}
