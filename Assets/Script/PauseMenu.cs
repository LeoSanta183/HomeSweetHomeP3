using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PauseButton;
    public GameObject ControlButton;
    public GameObject ControlScreen;
    public GameObject OptionsPanel;
    public GameObject crosshair;
    public GameObject HUD;
    public static bool GameIsPaused = false;
    public static bool ControlsIsOn = false;
    public static bool OptionsIsOn = false;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _resumeFirst;
    [SerializeField] private GameObject _settingsMenuFirst;

    // Panels for different options tabs
    public GameObject modeSettingsPanel;
    public GameObject creditsSettingsPanel;

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
        }
        if (Input.GetButtonDown("Cancel"))
        {
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
        HUD.SetActive(false);
        audioManager.PlaySFX(audioManager.buttonHover);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(_resumeFirst);
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        PauseButton.SetActive(true);
        crosshair.SetActive(true);
        ControlButton.SetActive(true);
        HUD.SetActive(true);
        audioManager.PlaySFX(audioManager.buttonHover);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EventSystem.current.SetSelectedGameObject(null);
    }
    
    public void Options()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = false;
        ControlsIsOn = true;
        PauseButton.SetActive(false);
        ControlButton.SetActive(false);
        ControlScreen.SetActive(false);
        OptionsPanel.SetActive(true);
        HUD.SetActive(false);
        audioManager.PlaySFX(audioManager.buttonHover);
        EventSystem.current.SetSelectedGameObject(_settingsMenuFirst);
    }

    public void Controls()
    {
        ControlButton.SetActive(false);
        audioManager.PlaySFX(audioManager.buttonHover);
        ControlScreen.SetActive(true);
        HUD.SetActive(false);
        ControlsIsOn = true;
        GameIsPaused = false;

    }
    public void Exit()
    {
        audioManager.PlaySFX(audioManager.buttonHover);
        Debug.Log("Returning to Main Menu...");
        SceneManager.LoadScene("HUB");
    }

    public void Quit()
    {
        audioManager.PlaySFX(audioManager.buttonHover);
        Debug.Log("Returning to Main Menu...");
        SceneManager.LoadScene("Main Menu");
    }

    public void ShowTab(GameObject tabPanel)
    {
        // Deactivate all option panels
        modeSettingsPanel.SetActive(false);
        creditsSettingsPanel.SetActive(false);
        GameIsPaused = false;

        // Activate the selected tab
        tabPanel.SetActive(true);
    }
}
