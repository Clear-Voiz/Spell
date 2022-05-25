using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public sealed class Player : NetworkBehaviour
{

    public static Player Instance { get; private set; }
    [SyncVar] public string username;

    [SyncVar] public bool isReady;

    [SyncVar] public Pawn controlledPawn;

    public Transform[] spawns;

    [SyncVar] private float contadorDeSpawns;
    
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
        /*Debug.Log("should work sweetheart");*/
    }

    private void Update()
    {
        if (!IsOwner) return;
        if(Input.GetKeyDown(KeyCode.R))
            ServerSetIsReady(!isReady);
    }

    [Server]
    public void StartGame()
    {
        if (GameManager.Instance == null) return;
        
        int playerIndex = GameManager.Instance.players.IndexOf(this);
        
        GameObject pawnPrefab = Resources.Load("Player1") as GameObject;


        if (pawnPrefab == null) return;

        var referencePoint = GameManager.Instance.spawnPoints[playerIndex];

        GameObject pawnInstance = Instantiate(pawnPrefab,referencePoint.position,referencePoint.rotation);
        
        Spawn(pawnInstance,Owner);
       
        
        

        /*Debug.Log(pawnInstance.transform.position);*/

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
