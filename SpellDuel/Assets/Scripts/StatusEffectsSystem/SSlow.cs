using FishNet.Object;
using UnityEngine;

public class SSlow : AlterSpell
{
    private StatModifier slow;

    private void Awake()
    {
        tim = new Timers(1);
        lifespan = 5f;
        effectTitle = "Slowed";
        IsBuff = false;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!IsOwner) return; 
        SlowDown();
    }

    [ServerRpc]
    public void SlowDown()
    {
        Stats stats = _conjure.stats;
        if (!stats.spd.statModifiers.Contains(slow))
        {
            slow = new StatModifier(-0.1f, modiType.Percent);
            stats.spd.AddModifier(slow);
            stats.cSpd = stats.spd.Value;
        }
    }

    private void Update()
    {
        if (!IsServer) return;
        Effect();
    }

    [Server]
    public override void Effect()
    {
        tim.alarm[0] = tim.Timer(lifespan,tim.alarm[0],EndEffect);
    }

    [Server]
    public override void EndEffect()
    {
        Stats stats = _conjure.stats;
        stats.spd.RemoveModifier(slow);
        stats.cSpd = stats.spd.Value;
        Debug.Log(stats.spd.statModifiers.Count);
        OnEnd();
    }
}
