using UnityEngine;

public class SSlow : AlterSpell
{
    private StatModifier slow;


    public SSlow()
    {
        tim = new Timers(1);
        lifespan = 5f;
        effectTitle = "Slowed";
        if (!Globs.spd.statModifiers.Contains(slow))
        {
            slow = new StatModifier(-0.1f, modiType.Percent);
            Globs.spd.AddModifier(slow);
        }
        OnStart();
    }
    
    public override void Effect()
    {
        tim.alarm[0] = tim.Timer(lifespan,tim.alarm[0],EndEffect);
    }

    public override void EndEffect()
    {
        Globs.spd.RemoveModifier(slow);
        Debug.Log(Globs.spd.statModifiers.Count);
        OnEnd();
    }
}
