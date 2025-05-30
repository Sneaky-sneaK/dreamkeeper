using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DreamerManager : MonoBehaviour
{
	public static DreamerManager Instance;

	public TextMeshProUGUI dreamerCounterText;
	public GameObject levelCompleteUI;

	private int totalDreamers;
	private int collectedDreamers;

    public AudioClip collectSound;
    private AudioSource audioSource;


    private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}

	private void Start()
	{
		// Count all Dreamers in the scene at start
		totalDreamers = FindObjectsByType<Dreamer>(FindObjectsSortMode.None).Length;
		UpdateUI();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = collectSound;
    }

	public void CollectDreamer(Dreamer dreamer)
	{
		collectedDreamers++;
        audioSource.Play();
        UpdateUI();


		if (collectedDreamers >= totalDreamers)
		{
			LevelComplete();
		}
	}

	private void UpdateUI()
	{
		int remaining = totalDreamers - collectedDreamers;
		dreamerCounterText.text = $"Dreamers Left: {remaining}";
	}

	private void LevelComplete()
	{
		PlayerHealth.IsDead = true;
        levelCompleteUI.SetActive(true);
	}

	public void GoToNextLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}

	public void QuitGame() {
		Debug.Log("Quitting Game");
		Application.Quit();
	}
}
