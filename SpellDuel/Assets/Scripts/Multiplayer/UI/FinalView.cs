using UnityEngine;
using UnityEngine.UI;

public class FinalView : View
{
    [SerializeField] private Button respawnButton;

    public override void Initialize()
    {
        base.Initialize();
        respawnButton.onClick.AddListener(()=> Player.Instance.RespawnPawn());
        
           
    }
}
