using UnityEngine;
using UnityEngine.UI;

public class FinalView : View
{
    [SerializeField] private Button respawnButton;
    [SerializeField] public Image[] hats;
    /*private int hatIndex = 0;
    private const int maxIndex = 2;*/

    public override void Initialize()
    {
        base.Initialize();
        //respawnButton.onClick.AddListener(()=> Player.Instance.RespawnPawn());

    }
    
    public void Rematch()
    {
        Player.Instance.OnRematch();
        /*hats[hatIndex].color = Color.green;
        hatIndex = (hatIndex + 1) % maxIndex;*/
    }

    public void Continue()
    {
        Player.Instance.OnContinue();
    }
}
