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
    }
    private void OnEnable()
    {
        print("Child onEnabled");
    }
    private void Start()
    {
        _bolt.SetFloat("Length",range);
        _bolt.SetFloat("Pivoter",-4.5f);
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
            LP = Resources.Load("LocalPoints") as GameObject;
            Debug.Log(LP);
            Instantiate(LP, other.transform.position, Quaternion.identity);
        }
        int damage = Mathf.RoundToInt((Globs.mgk.Value - stats.magicDef)*PM*stats.RES[Element]);

        if (stats.hp > damage)
        {
            stats.hp -= damage;
            Debug.Log(stats.hp + " mgk:" + Globs.mgk.Value + ", mgkDef" + stats.magicDef + ", PM" + PM + ", RES" + stats.RES[Element]);
        }
        else
        {
            stats.hp = 0f;
            Debug.Log(stats.hp);
            //Destroy(other.gameObject);
        }
    }
}
