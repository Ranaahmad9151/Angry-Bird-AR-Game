using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
  public static event Action<bool> OnLaunched = delegate { };
  public static event Action<Vector3, Vector3> OnDrag = delegate { };
  protected float mZCoord;
  protected Vector3 touchStartPoint;

  private bool isGamePlayActivated = false;


  #region Unity Events

  private void OnEnable()
  {
    GamePlayState.OnGamePlayActivated += GamePlayActivated;
  }
  private void OnDisable()
  {
    GamePlayState.OnGamePlayActivated -= GamePlayActivated;
  }
  #endregion


  #region Drag functions
  protected void OnMouseDown()
  {
    if (!isGamePlayActivated)
      return;

    mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

    touchStartPoint = GetMouseWorldPos();

  }
  protected void OnMouseUp()
  {

    if (!isGamePlayActivated)
      return;

    var dragDistance = GetMouseWorldPos() - touchStartPoint;

    //if drag is small or upwards
    var isDragBelowThreshold = dragDistance.sqrMagnitude < 0.01f || dragDistance.y > 0;

    touchStartPoint = Vector3.zero;
    OnLaunched(isDragBelowThreshold);


  }
  protected Vector3 GetMouseWorldPos()
  {
    var mousePoint = Input.mousePosition;
    mousePoint.z = mZCoord;

    return Camera.main.ScreenToWorldPoint(mousePoint);
  }
  protected void OnMouseDrag()
  {
    if (!isGamePlayActivated)
      return;

    var worldPos = GetMouseWorldPos();
    OnDrag(worldPos, touchStartPoint);

  }

  #endregion

  private void GamePlayActivated(bool isActive)
  {
    isGamePlayActivated = isActive;
  }
}
