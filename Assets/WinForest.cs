using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinForest : MonoBehaviour
{
    PlayerMovement pMovement;
    // Start is called before the first frame update
    void Start()
    {
        pMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CompleteLevel("Canyon");
            pMovement.CoinCollect();
        }
    }
    public void CompleteLevel(string Canyon)
    {
        // Save completion status
        PlayerPrefs.SetInt(Canyon + "_Completed", 1); // 1 means completed, 0 means not completed
        Debug.Log("Level " + Canyon + " completed!");

        // Load hub scene
        SceneManager.LoadScene("Hub");
    }

    public bool IsLevelCompleted(string Canyon)
    {
        // Check if the level is completed
        return PlayerPrefs.GetInt(Canyon + "_Completed", 0) == 1;
    }

}

