using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;       // Reference to the player's Transform
    public float moveSpeed = 2f;   // Speed at which the enemy moves
    public float detectionRange = 5f; // Distance to start moving
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").gameObject.transform;

        // Log a warning if player is null
        if (player == null)
        {
            Debug.LogWarning("Player Transform not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Move only if the player is within detection range
            if (distanceToPlayer <= detectionRange)
            {
                MoveTowardsPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }
}
