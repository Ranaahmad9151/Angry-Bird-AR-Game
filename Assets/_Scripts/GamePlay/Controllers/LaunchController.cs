using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchController
{
  private int throwPower;
  public LaunchController(int throwPower) => this.throwPower = throwPower;
  public void Launch(ThrowObject throwItem)
  {
    throwItem.transform.localRotation = Quaternion.identity;
    throwItem.gameObject.transform.parent = null;
    throwItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    throwItem.gameObject.GetComponent<Rigidbody>().velocity = throwItem.gameObject.transform.transform.forward * throwPower;
  }
}
