using System;
using UnityEngine;

public abstract class AlterSpell
{
    protected Timers tim;
    protected float lifespan;
    public string effectTitle = string.Empty;
    public static event Action<AlterSpell> onEnd;
    public static event Action<AlterSpell> onStart;
    
    public bool IsBuff {get; protected set;}

    public abstract void Effect();
    public abstract void EndEffect();

    protected void OnEnd()
    {
        onEnd?.Invoke(this);
        
    }
    
    protected void OnStart()
    {
        onStart?.Invoke(this);
        
    }
}
