using UnityEngine;

public class SafeZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered Safe Zone");
            LightMeter.SetInSafeZone(true);
            FindAnyObjectByType<PlayerMovement>().EnableLightWeapon(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Left Safe Zone");
            LightMeter.SetInSafeZone(false);
        }
    }
}
