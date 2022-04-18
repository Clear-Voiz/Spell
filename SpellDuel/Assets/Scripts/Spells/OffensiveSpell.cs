using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveSpell : Spell
{
    protected bool controllable;
   /* public OffensiveSpell(GameObject _VFX, GameObject _ImpactVFX, float _lifespan, float _speed, float _PM, float _cost, Elements _element,bool _controllable) : base(_VFX, _ImpactVFX, _lifespan, _speed, _PM, _cost, _element)
    {
        controllable = _controllable;
    }*/

    public override void Move()
    {
        if (controllable) 
        {transform.forward = dir.forward;}
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    public override void Impact(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            var stats = other.gameObject.GetComponent<Stats>();
            if (stats.hp > (Globs.mgk.Value - stats.magicDef))
            {
                stats.hp -= Mathf.Round((Globs.mgk.Value - stats.magicDef)*PM*stats.RES[Element]);
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
