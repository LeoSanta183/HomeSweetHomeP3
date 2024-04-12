using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoToForest : MonoBehaviour
{
    public TextMeshProUGUI promptText; // Reference to the TextMeshPro text component



    private bool inRange = false; // Flag to track if player is in range
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
            promptText.text = "Press 'E' to go to Forest";
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
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {

        }
    }
}
