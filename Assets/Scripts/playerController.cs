using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Joystick joystick;
    [SerializeField] private Transform head;
    [SerializeField] private Transform enemy;  // Change this to track multiple enemies
    public float detectionRadius = 5f;
    private Transform closestEnemy;
    [SerializeField] GameObject[] body;
    [SerializeField] Animator playerAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = new Vector2(joystick.Horizontal, joystick.Vertical);

        closestEnemy = FindClosestEnemy();

      

        if (closestEnemy != null && Vector3.Distance(transform.position, closestEnemy.position) <= detectionRadius)
        {
            RotateTowards(closestEnemy.position);
        }
        else if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            head.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        float normalizedJoystick = (Mathf.Abs(joystick.Horizontal) + Mathf.Abs(joystick.Vertical));
        playerAnimator.SetFloat("walk", normalizedJoystick);
        if (joystick.Horizontal < 0)
        {
            for (int i = 0; i < body.Length; i++)
            {
                body[i].transform.localScale = new Vector2(-1, 1);

            }
        }
        else if (joystick.Horizontal > 0)
        {
            for (int i = 0; i < body.Length; i++)
            {
                body[i].transform.localScale = new Vector2(1, 1);

            }


        }
    }

    Transform FindClosestEnemy()
    {
        float nearestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;
        foreach (Transform enemy in GameObject.FindObjectsOfType<Transform>())   // Assuming enemies have the Transform component
        {
            if (enemy.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, enemy.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }
        return nearestEnemy;
    }

    void RotateTowards(Vector3 targetPosition)
    {
        Vector2 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        head.rotation = Quaternion.Euler(0, 0, angle);
    }
}
