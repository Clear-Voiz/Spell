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

    public Transform[] spawnPoints;
    

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

    public override void OnStopClient()
    {
        base.OnStopClient();
        StopGame();
    }

    [Server]
    public void StartGame()
    {
        if (!canStart) return;
        if (players!=null && players.Count >0) Debug.Log(players.Count);
        foreach (var pl in players)
        {
            pl.StartGame();
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
}
