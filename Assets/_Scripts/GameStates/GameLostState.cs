using System;
using UnityEngine;

public class GameLostState : BaseState
{
  public static event Action OnGameLost = delegate { };
  public override void OnActivate()
  {
    Debug.Log("lost");
    OnGameLost();
    Managers.UI.EnableMenuPanel();
    Managers.UI.DisableStartButton();
    Managers.UI.EnableRestartButton();
    Managers.UI.EnableScoreText();
    Managers.UI.EnableLostText();

  }

  public override void OnDeactivate()
  {
    Managers.UI.DisableMenuPanel();
    Managers.UI.DisableStartButton();
    Managers.UI.DisableScoreText();
    Managers.UI.DisableLostText();
  }


}
