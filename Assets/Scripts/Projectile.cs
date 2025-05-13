using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    void Update() => transform.Translate(Vector3.forward * speed * Time.deltaTime);
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}