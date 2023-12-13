using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectContainer : MonoBehaviour
{
  [SerializeField] private List<ThrowObject> objectList;
  private List<ThrowObject> launchedObjects = new List<ThrowObject>();
  private List<Vector3> objectPositions = new List<Vector3>();


  private void Awake()
  {
    for (int i = 0; i < objectList.Count; i++)
    {
      objectPositions.Add(objectList[i].transform.localPosition);
    }
  }
  private void OnEnable()
  {
    ResetState.OnReset += ResetThrowObjects;
  }
  private void OnDisable()
  {
    ResetState.OnReset -= ResetThrowObjects;
  }
  public ThrowObject GetTopObject()
  {
    ThrowObject item = objectList[0];

    //for reset game saving launched balls
    launchedObjects.Add(item);

    objectList.RemoveAt(0);
    return item;
  }


  public void PrepareTopObject() => StartCoroutine(MoveUp());

  IEnumerator MoveUp()
  {
    yield return new WaitForSeconds(.5f);
    for (int i = 0; i < objectList.Count; i++)
    {
      objectList[i].transform.localPosition = new Vector3(objectPositions[i].x, objectPositions[i].y, objectPositions[i].z);
    }
    yield return new WaitForEndOfFrame();

    if (objectList.Count > 0)
      objectList[0].gameObject.GetComponent<Rigidbody>().isKinematic = false;
  }

  private void ResetThrowObjects()
  {
    foreach (var item in launchedObjects)
    {
      item.GetComponent<Rigidbody>().isKinematic = true;
      item.GetComponent<Collider>().enabled = true;
      item.transform.rotation = Quaternion.identity;
      objectList.Add(item);
      item.transform.SetParent(transform);
      item.transform.localPosition = objectPositions[objectList.Count - 1];
    }
    launchedObjects.Clear();
  }
}
