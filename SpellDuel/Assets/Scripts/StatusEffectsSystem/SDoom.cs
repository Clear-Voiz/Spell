using FishNet.Object;
using UnityEngine;

public class SDoom : AlterSpell
{
    private float PM;
    
    /*private void Start()
    {
        
    }*/

    public override void OnStartServer()
    {
        base.OnStartServer();
        _conjure.effectsManager.AddDebuff(this);
        lifespan = (float)_conjure.stats.lvl;
        
        tim = new Timers(1);
        PM = 0;
        IsBuff = false;
    }

    private void Update()
    {
        if (!IsServer) return;
        Effect();
    }

    
    public override void Effect()
    {
        if(Owner != _conjure.Owner) return;
        PM = tim.alarm[0];
        
        tim.alarm[0] = tim.Timer(lifespan,tim.alarm[0], Dispel);
    }

    [Server]
    public void Dispel()
    {
        EndEffect();
    }
    
    
    public override void EndEffect()
    {
        if(Owner != _conjure.Owner) return;
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
