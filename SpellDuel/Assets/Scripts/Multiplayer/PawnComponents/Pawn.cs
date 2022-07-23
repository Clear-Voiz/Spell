using System;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public sealed class Pawn : NetworkBehaviour
{
    [SyncVar] public Player controllingPlayer;

    [SyncVar] public float health;

    public GameObject followTargetPosition;
    public GameObject LookAtPosition;
    
    public static event Action<Transform> OnFirstObjectSpawned;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (IsOwner)
        {
            OnFirstObjectSpawned?.Invoke(transform);
        }
    }

    /*public void ReceiveDamage(float amount)
    {
        if (!IsSpawned) return;
        if ((health-=amount) <= 0f)
        {
            controllingPlayer.TargetPawnKilled(Owner);
            Despawn();
        }
    }*/
}
