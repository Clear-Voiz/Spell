using TMPro;
using UnityEngine;

public sealed class MainView : View
{

  [SerializeField] private TextMeshProUGUI healthText;
  

  private void Update()
  {
    if (!IsInitialized) return;

    Player player = Player.Instance;
    /*if (player == null || player.controlledPawn == null) return;
    healthText.text = $"Health {player.controlledPawn.health}";*/
  }
}
