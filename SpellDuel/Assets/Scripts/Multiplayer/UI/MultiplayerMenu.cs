using System;using System.Collections;
using System.Collections.Generic;
using System.Net;
using FishNet;
using FishNet.Discovery;
using UnityEngine;
using UnityEngine.UI;

public sealed class MultiplayerMenu : MonoBehaviour
{
    [SerializeField] private Button hostButt;

    [SerializeField] private Button connectButt;

    [SerializeField] private NetworkDiscovery networkDiscovery;
    private readonly List<IPEndPoint> _endPoints = new List<IPEndPoint>();

    private void Awake()
    {
        networkDiscovery = FindObjectOfType<NetworkDiscovery>();
    }

    private void Start()
    {
        networkDiscovery.ServerFoundCallback += (endPoint) =>{
            if (!_endPoints.Contains(endPoint)) _endPoints.Add(endPoint);
        };
        
        hostButt.onClick.AddListener(() =>
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
            //networkDiscovery.StartAdvertisingServer();
        });
        
        connectButt.onClick.AddListener(()=>
        {
            //networkDiscovery.ServerFoundCallback += ServerFound;
            networkDiscovery.StartSearchingForServers();
            
        });
    }

    private void Update()
    {
        if (InstanceFinder.ClientManager.Started) return;
        if (_endPoints.Count > 0)
        {
            if (InstanceFinder.ClientManager.Started == false)
            {
                //Debug.Log("I'm trying okay?");
                var ipAddress = _endPoints[0].Address.ToString();
                for (var i = 0; i < _endPoints.Count; i++)
                {

                    Debug.Log(ipAddress);
                    Debug.Log(_endPoints.Count);

                    networkDiscovery.StopAdvertisingServer();
                    networkDiscovery.StopSearchingForServers();
                    
                    InstanceFinder.ClientManager.StartConnection(ipAddress);
                }
            }
        }
    }

    private IEnumerator TrulyConnect()
{
    Debug.Log(_endPoints.Count);
    if (_endPoints.Count > 0)
    {
        while (InstanceFinder.ClientManager.Started == false)
        {
            Debug.Log("I'm trying okay?");
            var ipAddress = _endPoints[0].Address.ToString();
            InstanceFinder.ClientManager.StartConnection(ipAddress);
            yield return null;
        }
    }
}

/*private void OnDisable()
{
    if (InstanceFinder.IsClientOnly)
    {networkDiscovery.ServerFoundCallback -= ServerFound;}
}*/

    private void ServerFound(IPEndPoint endPoint)
    {
        /*string ipAdress = endPoint.ToString();
        Debug.Log(ipAdress);
        //networkDiscovery.StopAdvertisingServer();
        //networkDiscovery.StopSearchingForServers();
        InstanceFinder.ClientManager.StartConnection(ipAdress);*/
        Debug.Log("coroutine reached");
        Debug.Log(InstanceFinder.ClientManager.Started);
        Debug.Log(_endPoints.Count);
        StartCoroutine(nameof(TrulyConnect));

    }

    /*private async Task AdvertisingServer()
    {
        bool ready = InstanceFinder.ServerManager.Started;
        Task.Yield();
    }*/
}
