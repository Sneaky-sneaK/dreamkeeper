using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public GameObject bulletPrefab;      // Assign your bullet prefab in the Inspector
    public Transform firePoint;          // Empty GameObject indicating where the bullet spawns
    public float bulletSpeed = 20f;      // Speed of the bullet
    public float lightToUseWhenFiring = 1f;

    private LightMeter lightMeter;

    private void Start()
    {
        lightMeter = FindAnyObjectByType<LightMeter>();
    }

    void Update()
    {
        if (PlayerHealth.IsDead || LightMeter.isInSafeZone || lightMeter.LightRemaining < 1) return;

        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        // Create bullet at firePoint position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Projectile>().speed = bulletSpeed;

        lightMeter.UseLight(lightToUseWhenFiring);
    }
}
