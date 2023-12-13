using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDetectionHandler : MonoBehaviour
{
    private ARPlaneManager arPlaneManager;

    void Start()
    {
        // Get the ARPlaneManager component
        arPlaneManager = GetComponentInParent<ARPlaneManager>();

        // Subscribe to the plane detection events
        arPlaneManager.planesChanged += OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var addedPlane in args.added)
        {
            // Handle added plane
            Debug.Log("Plane added: " + addedPlane);
        }

        foreach (var updatedPlane in args.updated)
        {
            // Handle updated plane
            Debug.Log("Plane updated: " + updatedPlane);
        }

        foreach (var removedPlane in args.removed)
        {
            // Handle removed plane
            Debug.Log("Plane removed: " + removedPlane);
        }
    }
}