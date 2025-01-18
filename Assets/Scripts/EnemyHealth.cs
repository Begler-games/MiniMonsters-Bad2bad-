using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Slider healthBar;
    public GameObject dropItemPrefab;  // Assign Drop Item Prefab here
    Animator enemyAnimator;
    public bool enemyDied = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            enemyDied = true;
            StartCoroutine(dieAnimation());
        }
    }
    IEnumerator dieAnimation()
    {
        yield return new WaitForSeconds(2);
        Die();
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        DropItem();
        Destroy(gameObject);
    }

    void DropItem()
    {
        // Instantiate the drop item at the enemy's position
        Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
    }
}
