using System;
using UnityEngine;

public class GameWonState : BaseState
{
  public static event Action OnGameWon = delegate { };
  public override void OnActivate()
  {
    Debug.Log("WON");
    OnGameWon();
    Managers.UI.EnableMenuPanel();
    Managers.UI.DisableStartButton();
    Managers.UI.EnableRestartButton();
    Managers.UI.EnableScoreText();
    Managers.UI.EnableWinText();
  }

  public override void OnDeactivate()
  {
    Managers.UI.DisableMenuPanel();
    Managers.UI.DisableStartButton();
    Managers.UI.DisableScoreText();
    Managers.UI.DisableWinText();
  }

}
