using UnityEngine;

public class Playershooting : MonoBehaviour
{
    public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    void Update()
    {
        if (PlayerHealth.IsDead || LightMeter.isInSafeZone) return;

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}

}