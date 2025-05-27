using UnityEngine;

public class Dreamer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DreamerManager.Instance.CollectDreamer(this);
            Destroy(gameObject);
        }
    }
}
