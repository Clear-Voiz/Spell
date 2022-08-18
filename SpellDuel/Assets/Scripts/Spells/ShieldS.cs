using System;
using UnityEngine;

public class ShieldS : Spell
{
    private void Awake()
    {
        lifespan = 5f;
        cooldown = lifespan + 0.2f;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
       
        StartCoroutine(DestroyAfter(lifespan));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;
        if (other.CompareTag("Spell"))
        {
            if (other.TryGetComponent(out Spell spell))
            {
                if (spell.Element == Elements.NonElemental)
                {
                    spell.speed = 0f;
                    //Destroy(other.gameObject);
                }
                else
                {
                    BonusPoints(Owner);
                    Despawner();
                }
            }
            else
            {
                Debug.Log("false");
            }
        }
    }
}
