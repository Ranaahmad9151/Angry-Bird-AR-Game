using System;
using UnityEngine;

public class ResetState : BaseState
{

  public static event Action OnReset = delegate { };
  public override void OnActivate()
  {
    //Reset all game Setting,UI / Gameobjects to original positions..
    Debug.Log("reseting");
    Managers.UI.ResetUI();
    Managers.Score.ResetScore();

    OnReset();
    //setState to GamePlayState
    Managers.Game.SetState(typeof(GamePlayState));
  }

  public override void OnDeactivate()
  {

  }
}