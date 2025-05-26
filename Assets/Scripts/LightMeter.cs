using UnityEngine;
using UnityEngine.UI;

public class LightMeter : MonoBehaviour
{
    public Slider lightSlider;
    public float maxLight = 100f;
    public float currentLight;

    public float drainRate = 5f;      // How fast light drains per second
    public float refillRate = 10f;    // How fast light refills in safe zone

    public static bool isInSafeZone = false;

    void Start()
    {
        currentLight = maxLight;
        lightSlider.maxValue = maxLight;
        lightSlider.value = currentLight;
    }

    void Update()
    {
        if (isInSafeZone || PlayerHealth.IsDead)
        {
            currentLight += refillRate * Time.deltaTime;
        }
        else
        {
            currentLight -= drainRate * Time.deltaTime;
        }

        currentLight = Mathf.Clamp(currentLight, 0f, maxLight);
        lightSlider.value = currentLight;

        if (currentLight <= 0f)
        {
            Debug.Log("Player consumed by darkness!");
            // You could call a death function here if you want
        }
    }

    public static void SetInSafeZone(bool value)
    {
        isInSafeZone = value;
    }
}
