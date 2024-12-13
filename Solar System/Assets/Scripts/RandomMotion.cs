using UnityEngine;

public class RandomMotion : MonoBehaviour
{
    public float moveRange = 0.5f;  // Max distance the object can move from its original position
    public float moveInterval = 0.5f;  // How often the direction changes (in seconds)

    private Vector3 initialPosition;
    private float timer;

    void Start()
    {
        // Save the object's initial position when the game starts
        initialPosition = transform.position;
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // If the timer exceeds the move interval, move the object randomly
        if (timer >= moveInterval)
        {
            // Reset the timer
            timer = 0f;

            // Calculate a random direction to move within the specified range
            Vector3 randomDirection = new Vector3(
                Random.Range(-moveRange, moveRange),
                Random.Range(-moveRange, moveRange),
                Random.Range(-moveRange, moveRange)
            );

            // Apply the random movement to the initial position
            transform.position = initialPosition + randomDirection;
        }
    }
}