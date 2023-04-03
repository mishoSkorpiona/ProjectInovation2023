using UnityEngine;

public class PlateController : MonoBehaviour
{
    public float speed = 5.0f; // The speed at which the plate moves
    public float destroyHeight = -5.0f; // The height at which the plate is destroyed

    private bool isHeld = false; // Whether the plate is currently held by the player
    private Vector3 startPosition; // The starting position of the plate
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void Update()
    {
        // If the plate falls below the destroy height, destroy it
        if (transform.position.y < destroyHeight)
        {
            Destroy(gameObject);
        }
    }

    public void PickUpPlate()
    {
        // Set the plate's velocity to zero and mark it as held
        rb.velocity = Vector3.zero;
        isHeld = true;
    }

    public void ReleasePlate()
    {
        // Reset the plate's position and velocity and mark it as not held
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        isHeld = false;
    }

    public bool IsHeld()
    {
        return isHeld;
    }

    public void MovePlate(Vector3 direction)
    {
        // Move the plate in the specified direction at the specified speed
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }
}