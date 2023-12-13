using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostTrigger : MonoBehaviour
{
  public static event Action OnFail = delegate { };

  private void OnTriggerEnter(Collider other) => OnFail();

}
