using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private ARSessionOrigin arSessionOrigin;

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arSessionOrigin = GetComponent<ARSessionOrigin>();
    }

    void Update()
    {
        // Check if the user taps the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Raycast from the touch position
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            // Perform the raycast against AR planes
            if (arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                // Get the hit pose and set the AR Session Origin's position
                Pose hitPose = hits[0].pose;
                arSessionOrigin.MakeContentAppearAt(transform, hitPose.position, hitPose.rotation);
            }
        }
    }
}
