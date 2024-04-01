using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PauseButton;
    public GameObject ControlButton;
    public GameObject ControlScreen;
    public GameObject OptionsPanel;
    public GameObject crosshair;
    public static bool GameIsPaused = false;
    public static bool ControlsIsOn = false;
    public static bool OptionsIsOn = false;
        AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (GameIsPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }

            if(ControlsIsOn)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        ControlsIsOn = false;
        OptionsIsOn = false;
        crosshair.SetActive(false);
        PauseButton.SetActive(false);
        ControlButton.SetActive(false);
        ControlScreen.SetActive(false);
        OptionsPanel.SetActive(false);
        audioManager.PlaySFX(audioManager.buttonHover);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        PauseButton.SetActive(true);
        crosshair.SetActive(true);
        ControlButton.SetActive(true);
        audioManager.PlaySFX(audioManager.buttonHover);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void Options()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = true;
        ControlsIsOn = true;
        PauseButton.SetActive(false);
        ControlButton.SetActive(false);
        ControlScreen.SetActive(false);
        OptionsPanel.SetActive(true);
        audioManager.PlaySFX(audioManager.buttonHover);
    }

    public void Controls()
    {
        ControlButton.SetActive(false);
        audioManager.PlaySFX(audioManager.buttonHover);
        ControlScreen.SetActive(true);
        ControlsIsOn = true;

    }
    public void Exit()
    {
        audioManager.PlaySFX(audioManager.buttonHover);
        Debug.Log("Returning to Main Menu...");
        SceneManager.LoadScene("Main Menu");
    }
}
