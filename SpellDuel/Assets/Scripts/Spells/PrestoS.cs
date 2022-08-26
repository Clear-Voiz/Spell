using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class PrestoS : AlterSpell
{
    private StatModifier presto;

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        effectTitle = "Haste";

    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        tim = new Timers(1);
        lifespan = 5f;
        IsBuff = true;
        
        Acceleration(Owner);
    }

    private void Update()
    {
        if(!IsServer) return;
        Effect();
    }

    public override void Effect()
    {
        if(Owner != _conjure.Owner) return;
        tim.alarm[0] = tim.Timer(lifespan, tim.alarm[0], EndEffect);
    }

    public override void EndEffect()
    {
        Dispell();
    }

    [Server]
    public void Dispell()
    {
        if (Owner != _conjure.Owner) return;
        Debug.Log("it had to end...");
        Stats stats = _conjure.stats;
        stats.spd.RemoveModifier(presto);
        stats.cSpd = stats.spd.Value;
        //Globs.spd.RemoveModifier(presto);
        OnEnd();
    }


    [Server]
    private void Acceleration(NetworkConnection conn)
    { if (conn != _conjure.Owner) return;
        presto = new StatModifier(1f, modiType.Percent);
        Stats stats = _conjure.stats;
        if (!stats.spd.statModifiers.Contains(presto))
        {
            stats.spd.AddModifier(presto);
            stats.cSpd = stats.spd.Value;
            //Globs.spd.AddModifier(presto);
            Debug.Log("has been added");
        }
    }
}