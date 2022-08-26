using System.Net;
using FishNet;
using FishNet.Discovery;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : View
{
    [SerializeField] private Button toggleReadyButton;
    [SerializeField] private Button startGameButton;
    [SerializeField] private TextMeshProUGUI toggleReadyButtonText;
    [SerializeField] private NetworkDiscovery networkDiscovery;

    private void Awake()
    {
        networkDiscovery = FindObjectOfType<NetworkDiscovery>();
    }

    private void Start()
    {
        
            if (InstanceFinder.IsHost) networkDiscovery.StartAdvertisingServer();
                
            //if(InstanceFinder.IsClientOnly) networkDiscovery.StartSearchingForServers();
    }

    /*private void OnEnable()
    {
        networkDiscovery.ServerFoundCallback += ServerSetIsReady;
    }

    private void OnDisable()
    {
        networkDiscovery.ServerFoundCallback -= ServerSetIsReady;
    }*/

    public override void Initialize()
    {
        toggleReadyButton.onClick.AddListener(()=>Player.Instance.ServerSetIsReady(!Player.Instance.isReady));
        if (InstanceFinder.IsHost)
        {
            startGameButton.onClick.AddListener(() => GameManager.Instance.StartGame());
            startGameButton.gameObject.SetActive(true);
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
        }
        
        base.Initialize();
    }

    private void Update()
    {
        if(!IsInitialized) return;
        toggleReadyButtonText.color = Player.Instance.isReady ? Color.green : Color.red;

        startGameButton.interactable = GameManager.Instance.canStart;
        
            if (Input.GetKeyDown(KeyCode.X))
            {
                InstanceFinder.ServerManager.StartConnection();
                InstanceFinder.ClientManager.StartConnection();
                //networkDiscovery.StartAdvertisingServer();
            }
        
            if (Input.GetKeyDown(KeyCode.C))
            {
                //networkDiscovery.StartSearchingForServers();
            }
    }

    private void ServerSetIsReady(IPEndPoint thing)
    {
        Player.Instance.ServerSetIsReady(true);
    }
}
