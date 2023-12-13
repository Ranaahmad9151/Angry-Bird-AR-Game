using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Platform : MonoBehaviour
{
  private BoxCollider boxCollider;

  private void Awake() => boxCollider = GetComponent<BoxCollider>();

  private void OnEnable()
  {
    Hole.OnTargetHit += DisableCollider;
    ThrowObject.OnLaunched += EnableCollider;
  }

  //unregister from throwable object events
  private void OnDisable()
  {
    Hole.OnTargetHit -= DisableCollider;
    ThrowObject.OnLaunched -= EnableCollider;
  }

  private void DisableCollider() => boxCollider.enabled = false;

  private void EnableCollider(bool isDragBelowThreshold) => boxCollider.enabled = true;

}