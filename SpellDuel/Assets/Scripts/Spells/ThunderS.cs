using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ThunderS : OffensiveSpell
{
    public string AAA;
    public ThunderS(float LifeSpan)
    {
        
        //VFX = Resources.Load("PS_FireBall") as GameObject;
        ImpactVFX = null;
        lifespan = LifeSpan;
        PM = 1.2f; //Power Multiplier
        cost = 2f;
        Element = Elements.Thunder;
        ActiveCol = "#ff0000ff";
        InactiveCol = "#800000ff";
    }

    public override void Impact(Collision other)
    {
        
    }

    public void Strike(Collider other)
    {
        var stats = other.GetComponent<Stats>();
        if (stats.hp > (Globs.mgk.Value - stats.magicDef))
        {
            if (stats.RES[Element] > 1f)
            {
                Debug.Log(LP);
                MonoBehaviour.Instantiate(LP, other.transform.position, Quaternion.identity);
            }
            stats.hp -= Mathf.Round((Globs.mgk.Value - stats.magicDef)*PM*stats.RES[Element]);
            Debug.Log(stats.hp + " mgk:" + Globs.mgk.Value + ", mgkDef" + stats.magicDef + ", PM" + PM + ", RES" + stats.RES[Element]);
        }
        else
        {
            stats.hp = 0f;
            Debug.Log(stats.hp);
            //Destroy(other.gameObject);
        }
    }

    public override void Move()
    {
        
    }

    public void Haha()
    {
        Debug.Log("Works Fine");
    }
}
