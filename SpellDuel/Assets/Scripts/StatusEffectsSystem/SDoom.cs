using System;
using FishNet.Object;
using UnityEngine;

public class SDoom : AlterSpell
{
    private float PM;
    
    private void Start()
    {
        lifespan = (float)_conjure.stats.lvl;
        _conjure.effectsManager.AddDebuff(this);
        tim = new Timers(1);
        PM = lifespan;
        IsBuff = false;
    }

    private void Update()
    {
        if (!IsOwner) return;
        Effect();
    }

    
    public override void Effect()
    {
        tim.alarm[0] = tim.Timer(lifespan,tim.alarm[0], EndEffect);
    }

   

    [ServerRpc]
    public override void EndEffect()
    {
        var stats = _conjure.stats;
        var tmpdmg = (Globs.mgk.Value - stats.mgkDef.Value) * PM * stats.RES[Elements.Dark];
        int damage = Mathf.RoundToInt(tmpdmg);
        
        if (damage < 1)
        {
            damage = 1;
        }
        
        stats.TakeDamage(Elements.Dark,damage);

        OnEnd();
    }
}
