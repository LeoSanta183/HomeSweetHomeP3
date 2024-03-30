using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButton : MonoBehaviour
{
     AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    public void LoadScene()
    {
        SceneManager.LoadScene("OptionsMenu");
         audioManager.PlaySFX(audioManager.buttonHover);
    }
}
