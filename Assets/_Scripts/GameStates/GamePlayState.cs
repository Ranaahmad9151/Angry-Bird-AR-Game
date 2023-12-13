using System;
using UnityEngine;

public class GamePlayState : BaseState
{
  public static event Action<bool> OnGamePlayActivated = delegate { };
  private Game currentGame;

  public override void OnActivate()
  {

    OnGamePlayActivated(true);
    currentGame = new Game();
  }
  public override void OnDeactivate()
  {
    OnGamePlayActivated(false);

  }
  private void OnEnable()
  {
    WinTrigger.OnSuccess += ShotSuccess;
    LostTrigger.OnFail += ShotFail;
  }
  private void OnDisable()
  {
    WinTrigger.OnSuccess -= ShotSuccess;
    LostTrigger.OnFail -= ShotFail;
  }
  private void ShotSuccess()
  {
    currentGame.ShotIsSuccessful();
  }
  private void ShotFail()
  {
    currentGame.ShotIsFail();
  }

}
