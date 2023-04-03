using UnityEngine;

public class Plate : MonoBehaviour
{
    private GameController gameController; // The GameController object

    private bool hasFallen = false; // Flag to indicate if the plate has fallen

    void Start()
    {
        gameController = FindObjectOfType<GameController>(); // Find the GameController object in the scene
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasFallen)
        {
            if (collision.gameObject.CompareTag("Ground")) // Check if the plate has hit the ground
            {
                hasFallen = true;
                gameController.Penalize(1); // Penalize the player for dropping the plate
                Destroy(gameObject); // Remove the plate from the scene
            }
            else if (collision.gameObject.CompareTag("Table")) // Check if the plate has been delivered to the table
            {
                hasFallen = true;
                gameController.AddPoints(1); // Add a point to the player's score for delivering the plate
                Destroy(gameObject); // Remove the plate from the scene
            }
        }
    }
}