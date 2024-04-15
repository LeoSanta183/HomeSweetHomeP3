using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GoToKingdom : MonoBehaviour
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
            promptText.text = "Press 'E' to go to Kingdom";
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
            LoadLevel("Kingdom");
        }
    }

    public void LoadLevel(string levelName)
    {
        if (IsLevelCompleted("Kingdom"))
        {
            Debug.Log("Level already completed!");
            promptText.text = "Level already completed!";
            // Add logic here to handle the case where the player tries to re-enter a completed level
            // For example, show a message or prevent entrance
        }
        else
        {
            SceneManager.LoadScene("Kingdom");
        }
    }

    public bool IsLevelCompleted(string Kingdom)
    {
        return PlayerPrefs.GetInt(Kingdom + "_Completed", 0) == 1;
    }
}