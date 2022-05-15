using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SDoom : AlterSpell
{
    private Collider col;
    private Elements Element;
    private float PM;
    private GameObject LP;
    private Conjure _conjure;

    public SDoom(Collider col, float pm, GameObject lp)
    {
        this.col = col;
        tim = new Timers(1);
        Element = Elements.NonElemental;
        PM = pm;
        LP = lp;
        OnStart();
    }
    public override void Effect()
    {
        tim.alarm[0] = tim.Timer(3f,tim.alarm[0], EndEffect);
    }

    public override void EndEffect()
    {
        var stats = col.gameObject.GetComponent<Stats>();
        var tmpdmg = (Globs.mgk.Value - stats.mgkDef.Value) * PM * stats.RES[Element];
        int damage = Mathf.RoundToInt(tmpdmg);
        
        if (damage < 0)
        {
            damage = 1;
        }
        
        if (stats.RES[Element] > 1f)
        {
            MonoBehaviour.Instantiate(LP, col.transform.position, Quaternion.identity);
        }
        
        if (stats.HP > damage)
        {
            stats.HP -= damage;
            Debug.Log(
                stats.HP + 
                " mgk:" + Globs.mgk.Value + 
                ", mgkDef" + stats.mgkDef.Value + 
                ", PM" + PM + 
                ", RES" + stats.RES[Element]
            );
        }
        else
        {
            stats.HP = 0f;
            Debug.Log(stats.HP);
            //Destroy(other.gameObject);
        }

        OnEnd();
    }
}
