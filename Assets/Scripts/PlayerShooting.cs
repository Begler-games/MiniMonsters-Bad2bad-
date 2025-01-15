using UnityEngine;
using System.Collections;
using TMPro;
public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public int ammo = 10;
    [SerializeField] TextMeshProUGUI ammoText;
    public void Shoot()
    {
        if (ammo > 0)
        {
            // Instantiate bullet at the fire point's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Corrected: Use `velocity` instead of `linearVelocity`
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = firePoint.right * bulletSpeed;

            ammo--;
            ammoText.text = "ammo: " + ammo;
            StartCoroutine(BulletDestroyTime(bullet));
        }
        else
        {
            StartCoroutine(ReloadWait());
        }
    }

    IEnumerator ReloadWait()
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(3);
        ammo += 10;
        ammoText.text = "ammo: " + ammo;
    }

    IEnumerator BulletDestroyTime(GameObject bullet)
    {
        yield return new WaitForSeconds(2);
        Destroy(bullet);
    }
}
