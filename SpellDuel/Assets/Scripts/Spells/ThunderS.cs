using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    private void Start()
    {
        Spell spell;
        RaycastHit hit;
        Physics.Raycast(_conjure.ori.position, _conjure.ori.forward, out hit);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Strike(hit.collider);
            }
            range = hit.distance;
            if (hit.collider.TryGetComponent(out spell))
            {
                if (spell is ShieldS)
                {
                    LP = _conjure.SH.LP;
                    Instantiate(LP, hit.point, Quaternion.identity);
                    Destroy(spell.gameObject);
                }
            }
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

        Destroy(gameObject,lifespan);
        StartCoroutine(Cine_Shake.Instance.shakeCamera(3f, 0.5f));
    }

    public void Strike(Collider other)
    {
        var stats = other.GetComponent<Stats>();
        if (stats.RES[Element] > 1f)
        {
            LP = _conjure.SH.LP;
            Instantiate(LP, other.transform.position, Quaternion.identity);
        }
        int damage = Mathf.RoundToInt((Globs.mgk.Value - stats.mgkDef.Value)*PM*stats.RES[Element]);

        if (stats.HP > damage)
        {
            stats.HP -= damage;
            Debug.Log(stats.hp + " mgk:" + Globs.mgk.Value + ", mgkDef" + stats.mgkDef.Value + ", PM" + PM + ", RES" + stats.RES[Element]);
        }
        else
        {
            stats.HP = 0f;
            Debug.Log(stats.HP);
            //Destroy(other.gameObject);
        }
    }
}
