using System;
using System.Linq;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public sealed class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SyncObject]
    public readonly SyncList<Player> players = new SyncList<Player>();

    [SyncVar] public bool canStart;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!IsServer) return;
        canStart = players.All(player => player.isReady);
        
        
        /*Debug.Log($"Can Start = {canStart}");*/
    }

    [Server]
    public void StartGame()
    {
        if (!canStart) return;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].StartGame();
        }
    }

    [Server]
    public void StopGame()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].StopGame();
        }
    }
}
