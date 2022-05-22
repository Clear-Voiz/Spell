using FishNet;
using UnityEngine;
using UnityEngine.UI;

public sealed class MultiplayerMenu : MonoBehaviour
{
    [SerializeField] private Button hostButt;

    [SerializeField] private Button connectButt;

    private void Start()
    {
        hostButt.onClick.AddListener(() =>
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        });
        
        connectButt.onClick.AddListener(()=> InstanceFinder.ClientManager.StartConnection());

    }
}
