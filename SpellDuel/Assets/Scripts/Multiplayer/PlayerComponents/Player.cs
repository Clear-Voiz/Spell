﻿using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.AddressableAssets;

public sealed class Player : NetworkBehaviour
{

    public static Player Instance { get; private set; }
    [SyncVar] public string username;

    [SyncVar] public bool isReady;

    [SyncVar] public Pawn controlledPawn;
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        GameManager.Instance.players.Add(this);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        GameManager.Instance.players.Remove(this);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!IsOwner) return;
        Instance = this;
        UIManager.Instance.Initialize();
        UIManager.Instance.Show<LobbyView>();
        Debug.Log("should work sweetheart");
    }

    private void Update()
    {
        if (!IsOwner) return;
        if(Input.GetKeyDown(KeyCode.R))
            ServerSetIsReady(!isReady);
    }

    public void StartGame()
    {
        GameObject pawnPrefab = Addressables.LoadAssetAsync<GameObject>("Pawn").WaitForCompletion();

        if (pawnPrefab == null) return;
        GameObject pawnInstance = Instantiate(pawnPrefab);
        
        
        Spawn(pawnInstance,Owner);

        controlledPawn = pawnInstance.GetComponent<Pawn>();

        controlledPawn.controllingPlayer = this;
        
        TargetPawnSpawned(Owner);
    }

    [ServerRpc(RequireOwnership = false)]
    public void RespawnPawn()
    {
       StartGame();
    }

    public void StopGame()
    {
        if (controlledPawn !=null && controlledPawn.IsSpawned) controlledPawn.Despawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerSetIsReady(bool value)
    {
        isReady = value;
    }

    [TargetRpc]
    private void TargetPawnSpawned(NetworkConnection conn = null)
    {
        UIManager.Instance.Show<MainView>();
    }

    [TargetRpc]
    public void TargetPawnKilled(NetworkConnection conn)
    {
     UIManager.Instance.Show<RespawnView>();   
    }

}
