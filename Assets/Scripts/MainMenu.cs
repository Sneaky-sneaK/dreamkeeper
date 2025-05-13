using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Loads the next scene in the build
    }

    public void OpenOptions()
    {
        // Implement your options menu logic here
        Debug.Log("Options button pressed"); // Example: Display a message in the console
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed"); // Example: Display a message in the console
        Application.Quit(); // Exits the application (only works in standalone builds)
    }
}