using UnityEngine;

public class MagnetField : MonoBehaviour
{
    public float magnetRadius = 5f;  // Radius of magnetic field

    void Start()
    {
        CircleCollider2D col = gameObject.AddComponent<CircleCollider2D>();
        col.isTrigger = true;
        col.radius = magnetRadius;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            ItemPickup itemPickup = other.GetComponent<ItemPickup>();
            if (itemPickup != null)
            {
                itemPickup.ActivateMagnet();  // Call ActivateMagnet method directly
            }
        }
    }
}
