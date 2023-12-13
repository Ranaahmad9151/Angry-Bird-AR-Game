using UnityEngine;

public class RestartButton : BaseButton
{
  public override void OnButtonPressed()
  {
    Managers.Game.SetState(typeof(ResetState));
  }
}
