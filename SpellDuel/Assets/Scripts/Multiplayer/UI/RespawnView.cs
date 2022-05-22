using UnityEngine;
using UnityEngine.UI;

public class RespawnView : View
{
    [SerializeField] private Button respawnButton;

    public override void Initialize()
    {
        base.Initialize();
        respawnButton.onClick.AddListener(()=> Player.Instance.RespawnPawn());
           
    }
}
