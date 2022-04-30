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
    public float speed;
    protected float PM; //Power Multiplier
    protected float cost;
    public Elements Element;
    protected string ActiveCol;
    protected string InactiveCol;
    protected GameObject LP;
    protected Conjure _conjure;
    
    
    protected void Damager(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            speed = 0f;
            var stats = other.gameObject.GetComponent<Stats>();
            var tmpdmg = (Globs.mgk.Value - stats.magicDef) * PM * stats.RES[Element];
            int damage = Mathf.RoundToInt(tmpdmg);
            if (stats.RES[Element] > 1f)
            {
                LP = Resources.Load("LocalPoints") as GameObject;
                Instantiate(LP, transform.position, Quaternion.identity);
            }
            if (stats.hp > damage)
            {
                stats.hp -= damage;
                Debug.Log(
                    stats.hp + 
                    " mgk:" + Globs.mgk.Value + 
                    ", mgkDef" + stats.magicDef + 
                    ", PM" + PM + 
                    ", RES" + stats.RES[Element]
                    );
            }
            else
            {
                stats.hp = 0f;
                Debug.Log(stats.hp);
                //Destroy(other.gameObject);
            }

            //Destroy(gameObject);
        }
        else
        {
            if (!other.CompareTag("Spell"))
            {
                speed = 0f;
                //Fade Away
            }
        }
    }
}
