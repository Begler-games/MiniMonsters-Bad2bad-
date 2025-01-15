using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public Sprite icon;
    public float moveSpeed = 5f;            // Speed at which the item moves towards the player
    private Transform playerTransform;      // Reference to the player's transform
    private bool isMagnetActive = false;    // Flag to activate magnet effect

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isMagnetActive)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void ActivateMagnet()
    {
        isMagnetActive = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(itemName, quantity, icon);
                Destroy(gameObject);  // Destroy the item after being picked up
            }
        }
        else if (other.CompareTag("MagnetField"))
        {
            ActivateMagnet();
        }
    }
}
