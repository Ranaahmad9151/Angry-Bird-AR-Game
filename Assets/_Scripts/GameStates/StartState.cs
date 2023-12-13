public class StartState : BaseState
{
  public override void OnActivate()
  {
    Managers.UI.EnableMenuPanel();
    Managers.UI.EnableStartButton();
    Managers.UI.DisableRestartButton();
  }

  public override void OnDeactivate()
  {
    Managers.UI.DisableMenuPanel();
    Managers.UI.DisableStartButton();
  }
}
