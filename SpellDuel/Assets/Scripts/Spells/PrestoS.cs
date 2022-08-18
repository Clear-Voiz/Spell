using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class PrestoS : AlterSpell
{
    private StatModifier presto;

    public override void OnStartClient()
    {
        base.OnStartClient();
        tim = new Timers(1);
        lifespan = 5f;
        effectTitle = "Haste";
        IsBuff = true;

    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        Acceleration(Owner);
    }

    private void Update()
    {
        if(!IsOwner) return;
        Effect();
    }

    public override void Effect()
    {
        tim.alarm[0] = tim.Timer(lifespan, tim.alarm[0], Dispell,Owner);
    }
    
    public override void EndEffect()
    {
        if(!IsOwner) return;
        Debug.Log("it had to end...");
        Globs.spd.RemoveModifier(presto);
        OnEnd();
    }

    [TargetRpc]
    public void Dispell(NetworkConnection conn)
    {
        EndEffect();
    }

    [TargetRpc]
    private void Acceleration(NetworkConnection conn)
    {
        presto = new StatModifier(1f, modiType.Percent);
        if (!Globs.spd.statModifiers.Contains(presto))
        {
            Globs.spd.AddModifier(presto);
            Debug.Log("has been added");
        }
    }
}