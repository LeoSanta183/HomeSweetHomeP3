using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GoToCanyon : MonoBehaviour
{
        public TextMeshProUGUI promptText;
    private bool inRange = false;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            ShowPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            HidePrompt();
        }
    }

    private void ShowPrompt()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = "Press 'E' to go to Canyon";
        }
    }

    private void HidePrompt()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (inRange && Input.GetButtonDown("Enter"))
        {
            LoadLevel("Canyon");
        }
    }

    public void LoadLevel(string levelName)
    {
        if (IsLevelCompleted(levelName))
        {
            Debug.Log("Level already completed!");
            promptText.text = "Level already completed!";
            // Add logic here to handle the case where the player tries to re-enter a completed level
            // For example, show a message or prevent entrance
        }
        else
        {
            SceneManager.LoadScene(levelName);
        }
    }

    public bool IsLevelCompleted(string levelName)
    {
        return PlayerPrefs.GetInt(levelName + "_Completed", 0) == 1;
    }
}
