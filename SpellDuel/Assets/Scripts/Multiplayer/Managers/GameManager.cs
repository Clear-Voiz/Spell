using System.Collections.Generic;
using System.Linq;
using FishNet.Discovery;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public sealed class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SyncObject]
    public readonly SyncList<Player> players = new SyncList<Player>();


    //only available on server
    public readonly List<Conjure> Conjurers;

    /*[SyncObject] public readonly SyncList<Conjure> conjureDatabase;
    */

    [SyncVar] public bool canStart;

    public Transform[] spawnPoints;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!IsServer) return;
        canStart = players.All(player => player.isReady);
        
        
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        StopGame();
    }

    [Server]
    public void StartGame()
    {
        if (!canStart) return;
        foreach (var pl in players)
        {
            pl.StartGame();
            pl.isReady = false;
        }
    }

    [Server]
    public void StopGame()
    {
        foreach (var pl in players)
        {
            pl.StopGame();
        }
    }

    [Server]
    public void EndGame()
    {
        foreach (var pl in players)
        {
            pl.StopGame();
        }
    }
    
    
}
