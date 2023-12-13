using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
  public static event Action OnSuccess = delegate { };

  private void OnTriggerEnter(Collider other)
  {
    other.enabled = false;
    OnSuccess();
  }

}
