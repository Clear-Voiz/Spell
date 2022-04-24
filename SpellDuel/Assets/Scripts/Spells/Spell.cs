using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum Elements{NonElemental = 0,Fire = 1,Ice = 2,Thunder = 4,Earth = 8,Water = 16,Wind = 32,Light = 64, Dark= 128}

public abstract class Spell: MonoBehaviour
{
    //var definitions
    protected GameObject VFX;
    protected GameObject ImpactVFX;
    protected float lifespan;
    protected float speed;
    protected float PM; //Power Multiplier
    protected float cost;
    public Elements Element;
    protected string ActiveCol;
    protected string InactiveCol;
    protected GameObject LP;
    protected Conjure _conjure;

    private void Start()
    {
        print("Parent Start");
    }

    private void OnEnable()
    {
        LP = Resources.Load("LocalPoints") as GameObject;
        print("Parent onEnabled");

    }

    public virtual void Damager(Collision other)
    {
        speed = 0f;
        if (other.collider.CompareTag("Enemy"))
        {
            var stats = other.gameObject.GetComponent<Stats>();
            int damage = Mathf.RoundToInt((Globs.mgk.Value - stats.magicDef)*PM*stats.RES[Element]);
            if (stats.RES[Element] > 1f)
            {
                LP = Resources.Load("LocalPoints") as GameObject;
                Instantiate(LP, transform.position, Quaternion.identity);
            }
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

            //Destroy(gameObject);
        } 
    }
}
