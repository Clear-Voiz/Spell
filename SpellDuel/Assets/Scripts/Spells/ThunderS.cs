﻿using System.Collections;
using FishNet.Connection;
using UnityEngine;
using UnityEngine.VFX;

public class ThunderS : Spell
{
    private VisualEffect _bolt;
    public float range;

    private void Awake()
    { 
       // _conjure = FindObjectOfType<Conjure>();
        _bolt = GetComponentInChildren<VisualEffect>();
        PM = 1.2f; //Power Multiplier
        Element = Elements.Thunder;
        _conjure = FindObjectOfType<Conjure>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Spell spell;
        RaycastHit hit;
        Physics.Raycast(_conjure.ori.position, _conjure.ori.forward, out hit);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Stats stats))
            {
                if (stats.IsOwner) return;
                Strike(hit.collider);
            }
            
            if (hit.collider.TryGetComponent(out spell))
            {
                if (spell is ShieldS)
                {
                    if (!IsOwner) return;
                    BonusPoints(LocalConnection);
                    spell.Despawner();
                }
            }
            range = hit.distance;
        }
        else
        {
            range = 30f;
        }
        _bolt.SetFloat("Length",range);
        _bolt.SetFloat("Pivoter",-4.4f);

        ImpactVFX = null;
        lifespan = _bolt.GetFloat("LifeSpan");
        cost = 2f;
        ActiveCol = "#ff0000ff";
        InactiveCol = "#800000ff";
        StartCoroutine(Cine_Shake.Instance.shakeCamera(3f, lifespan-0.1f));
        if (!IsOwner) return;
        StartCoroutine(Cleaner());
    }

    public override void OnSpawnServer(NetworkConnection connection)
    {
        base.OnSpawnServer(connection);
    }

    private void Strike(Collider other)
    {
        var stats = other.GetComponent<Stats>();
        
        int damage = Mathf.RoundToInt((Globs.mgk.Value - stats.mgkDef.Value)*PM*stats.RES[Element]);
        Inflict(stats,damage,LocalConnection);
    }
    
    private IEnumerator Cleaner()
    {
        yield return new WaitForSeconds(lifespan);
        Debug.Log("should Clean 2");
        Despawner();
    }
}
