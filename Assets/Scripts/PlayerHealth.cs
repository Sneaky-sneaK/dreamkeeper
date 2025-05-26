using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Image[] hearts; // Assign the 3 heart Images in the Inspector
    public Color fullColor = Color.white;
    public Color emptyColor = new Color(0.5f, 0.5f, 0.5f, 0.5f); // Grey + semi-transparent

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHearts();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].color = fullColor;
            }
            else
            {
                hearts[i].color = emptyColor;
            }
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");
        // Implement death behavior here (e.g., disable player, load game over screen)
    }
}
