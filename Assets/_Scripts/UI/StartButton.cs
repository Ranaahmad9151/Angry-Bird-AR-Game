using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : BaseButton
{
    private void Start()
    {
        OnButtonPressed();
    }
    public override void OnButtonPressed()
  {
    Managers.Game.SetState(typeof(GamePlayState));
  }
}
