using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
  public abstract void OnActivate();
  public abstract void OnDeactivate();
}