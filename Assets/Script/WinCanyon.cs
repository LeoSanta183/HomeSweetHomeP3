using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCanyon : MonoBehaviour
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
            CompleteLevel("Forest");
            pMovement.CoinCollect();
        }
    }
    public void CompleteLevel(string Forest)
    {
        // Save completion status
        PlayerPrefs.SetInt(Forest + "_Completed", 1); // 1 means completed, 0 means not completed
        Debug.Log("Level " + Forest + " completed!");

        // Load hub scene
        SceneManager.LoadScene("Hub");
    }

    public bool IsLevelCompleted(string Forest)
    {
        // Check if the level is completed
        return PlayerPrefs.GetInt(Forest + "_Completed", 0) == 1;
    }

}
