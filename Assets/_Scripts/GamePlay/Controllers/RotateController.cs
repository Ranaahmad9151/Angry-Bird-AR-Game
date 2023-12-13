using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController
{

  private int rotationSensibility;
  private int maxRotateVertical;
  private int maxRotateHorizontal;
  public RotateController(int rotationSensibility, int maxRotateVertical, int maxRotateHorizontal)
  {
    this.rotationSensibility = rotationSensibility;
    this.maxRotateVertical = maxRotateVertical;
    this.maxRotateHorizontal = maxRotateHorizontal;
  }

  public void Rotate(Vector3 endDragPos, Vector3 startDragPos, Transform rotateObject)
  {

    var dragAmount = startDragPos - endDragPos;

    if (dragAmount.y < 0) return;

    var rotateAngle = dragAmount * rotationSensibility;

    //Clamping angles
    var verticalRotationClamped = rotateAngle.y > maxRotateVertical ? maxRotateVertical : rotateAngle.y;
    var horizontalRotationClamped = Mathf.Clamp(rotateAngle.x, -maxRotateHorizontal, maxRotateHorizontal);

    //Rotation  
    rotateObject.eulerAngles = new Vector3(-verticalRotationClamped, horizontalRotationClamped, rotateObject.eulerAngles.z);
  }
}
