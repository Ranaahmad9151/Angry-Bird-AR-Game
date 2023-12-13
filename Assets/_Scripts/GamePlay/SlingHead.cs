using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingHead : MonoBehaviour
{
  [SerializeField] private float animDuration;

  //register to throwable object events
  private void OnEnable() => ThrowObject.OnLaunched += MoveDown;

  //unregister from throwable object events
  private void OnDisable() => ThrowObject.OnLaunched -= MoveDown;

  private void MoveDown(bool isDragBelowThreshold)
  {
    if (isDragBelowThreshold) return;
    StartCoroutine(MoveDownUpAnim());
  }

  IEnumerator MoveDownUpAnim()
  {
    var startPosition = transform.localPosition;
    var endPosition = new Vector3(startPosition.x, startPosition.y - 0.18f, startPosition.z);

    var t = 0f;
    while (t < 1)
    {
      t += Time.deltaTime / animDuration;
      transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
      yield return null;
    }

    t = 0f;
    while (t < 1)
    {
      t += 2 * Time.deltaTime / animDuration;
      transform.localPosition = Vector3.Lerp(endPosition, startPosition, t);
      yield return null;
    }
  }
}
