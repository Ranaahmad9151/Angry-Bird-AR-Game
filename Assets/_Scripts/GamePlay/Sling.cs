using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sling : MonoBehaviour
{
  #region Properties

  [Header("GAMEPLAY SETTINGS")]
  [SerializeField] [Range(0, 30)] private int maxRotateVertical;
  [SerializeField] [Range(0, 60)] private int maxRotateHorizontal;
  [SerializeField] private int rotationSensibility;
  [SerializeField] private float bounceOutAnimDuration;


  [Header("REFERENCES")]
  [SerializeField] private ThrowObjectContainer objectContainer;


  [Header("LINE PATH")]
  [SerializeField] private LineRenderer lineVisual;
  [SerializeField] private int lineSegment = 10;
  [SerializeField] private Transform targetingPoint;
  private ThrowObject throwItem;
  [SerializeField] private int throwPower = 20;


  //Sling Behaviour Controllers
  private LaunchController launchController;
  private RotateController rotateController;
  private PathController pathController;
  #endregion

  #region Unity events

  private void Awake()
  {
    launchController = new LaunchController(throwPower);
    rotateController = new RotateController(rotationSensibility, maxRotateVertical, maxRotateHorizontal);
    pathController = new PathController(lineSegment, lineVisual);
  }
  private void Start()
  {
    lineVisual.positionCount = lineSegment;
    throwItem = objectContainer.GetTopObject();
  }

  //register to throwable object events
  private void OnEnable()
  {
    ThrowObject.OnLaunched += Launch;
    ThrowObject.OnDrag += RotateAndShowPath;
    ResetState.OnReset += ResetSling;
  }

  //unregister from throwable object events
  private void OnDisable()
  {
    ThrowObject.OnLaunched -= Launch;
    ThrowObject.OnDrag -= RotateAndShowPath;
    ResetState.OnReset -= ResetSling;
  }

  #endregion

  private void Launch(bool isDragBelowThreshold)
  {
    lineVisual.enabled = false;
    VibrationAnimToOriginalPosAfterLaunch();

    if (isDragBelowThreshold) return;

    launchController.Launch(throwItem);
    objectContainer.PrepareTopObject();
    throwItem = null;

  }

  private void RotateAndShowPath(Vector3 endDragPos, Vector3 startDragPos)
  {

    Rotate(endDragPos, startDragPos);
    DrawPath(endDragPos, startDragPos);
  }

  private void Rotate(Vector3 endDragPos, Vector3 startDragPos) => rotateController.Rotate(endDragPos, startDragPos, this.transform);


  private void DrawPath(Vector3 endDragPos, Vector3 startDragPos)
  {
    if (throwItem == null)
      throwItem = objectContainer.GetTopObject();

    lineVisual.enabled = true;

    var initialVelocity = throwItem.transform.forward * throwPower;
    throwItem.transform.localRotation = Quaternion.identity;
    pathController.VisualizePath(initialVelocity, throwItem.transform.position);
  }
  private void VibrationAnimToOriginalPosAfterLaunch() => StartCoroutine(Move());

  IEnumerator Move()
  {
    Vector3 startPosition = transform.position;
    Quaternion startRotation = transform.rotation;

    var t = 0f;
    while (t < 1)
    {
      t += Time.deltaTime / bounceOutAnimDuration;
      transform.rotation = Quaternion.Lerp(startRotation, Quaternion.identity, Ease.BounceEaseOut(t));
      yield return null;
    }
  }

  private void ResetSling()
  {
    VibrationAnimToOriginalPosAfterLaunch();
    lineVisual.enabled = false;
    objectContainer.PrepareTopObject();
    throwItem = null;

  }

}



