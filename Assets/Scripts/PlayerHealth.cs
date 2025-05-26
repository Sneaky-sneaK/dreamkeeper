using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Image[] hearts; // Assign the 3 heart Images in the Inspector
    public Color fullColor = Color.white;
    public Color emptyColor = new Color(0.5f, 0.5f, 0.5f, 0.5f); // Grey + semi-transparent

    public GameObject deathScreen; // Drag your DeathScreen panel into this field in the Inspector
    public static bool IsDead;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
        PlayerHealth.IsDead = false;
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
        PlayerHealth.IsDead = true;
        Debug.Log("Player has died.");
        deathScreen.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
