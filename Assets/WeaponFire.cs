using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public GameObject bulletPrefab;      // Assign your bullet prefab in the Inspector
    public Transform firePoint;          // Empty GameObject indicating where the bullet spawns
    public float bulletSpeed = 20f;      // Speed of the bullet

    void Update()
    {
        if (PlayerHealth.IsDead || LightMeter.isInSafeZone) return;

        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        // Create bullet at firePoint position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Apply velocity to the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }
}
