using FishNet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : View
{
    [SerializeField] private Button toggleReadyButton;
    [SerializeField] private Button startGameButton;
    [SerializeField] private TextMeshProUGUI toggleReadyButtonText;

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
    }
}
