using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    public Transform bird; // Reference to the bird GameObject
    public float maxPullDistance = 2f; // Maximum distance the slingshot can be pulled
    public float launchForce = 10f; // Force applied to the bird on release

    private Vector3 originalBirdLocalPosition;
    public bool isDragging = false;

    void Start()
    {
        // Save the original local position of the bird relative to its parent
        originalBirdLocalPosition = bird.localPosition;
    }

    void Update()
    {

        if (isDragging)
        {
            // Calculate the pull distance based on the touch position
            float pullDistance = Mathf.Min(Vector3.Distance(originalBirdLocalPosition, GetTouchPosition()), maxPullDistance);

            // Adjust the local position of the bird based on the pull distance
            bird.localPosition = originalBirdLocalPosition - bird.up * pullDistance;
        }

        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Check if the touch is initiated on the bird
                    if (IsTouchingBird(touch.position))
                    {
                        isDragging = true;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        // Continue updating the local position of the bird while dragging
                        float pullDistance = Mathf.Min(Vector3.Distance(originalBirdLocalPosition, GetTouchPosition()), maxPullDistance);
                        bird.localPosition = originalBirdLocalPosition - bird.up * pullDistance;
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        // Launch the bird when the touch is released
                        LaunchBird();
                        isDragging = false;
                    }
                    break;
            }
        }
    }

    Vector3 GetTouchPosition()
    {
        // Convert screen position to world position
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        print("Object " + ray);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
        /*RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Destroy(hit.transform.gameObject);
            }
        }*/
    }

    bool IsTouchingBird(Vector2 touchPosition)
    {
        // Perform a raycast to check if the touch is on the bird
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == bird;
        }

        return false;
    }

    void LaunchBird()
    {
        // Calculate the launch direction based on the pulled distance
        Vector3 launchDirection = (originalBirdLocalPosition - bird.localPosition).normalized;

        // Apply force to the bird to launch it
        bird.GetComponent<Rigidbody>().AddForce(launchDirection * launchForce, ForceMode.Impulse);

        // Reset the local position of the bird to its original position relative to its parent
        bird.localPosition = originalBirdLocalPosition;
    }
}
