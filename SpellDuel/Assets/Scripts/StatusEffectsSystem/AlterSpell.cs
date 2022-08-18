using System;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public abstract class AlterSpell : NetworkBehaviour
{
    protected Timers tim;
    protected float lifespan;
    public string effectTitle = string.Empty;
    [SyncVar] public Conjure _conjure;
    protected NetworkConnection Caller;
    public static event Action<AlterSpell> onEnd;

    public bool IsBuff {get; protected set;}

    /*public override void OnStartServer()
    {
        base.OnStartServer();
        Caller = _conjure.Owner;
    }*/

    public abstract void Effect();
    public abstract void EndEffect();

    protected void OnEnd()
    {
        onEnd?.Invoke(this);
        
    }
}
