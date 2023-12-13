using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
  public BoxCollider platformCollider;
  private void OnTriggerEnter(Collider other) => platformCollider.enabled = true;

}
