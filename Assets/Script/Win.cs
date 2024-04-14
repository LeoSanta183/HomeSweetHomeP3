using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
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
            CompleteLevel("Kingdom");
            pMovement.CoinCollect();
        }
    }
    public void CompleteLevel(string Kingdom)
    {
        // Save completion status
        PlayerPrefs.SetInt(Kingdom + "_Completed", 1); // 1 means completed, 0 means not completed
        Debug.Log("Level " + Kingdom + " completed!");

        // Load hub scene
        SceneManager.LoadScene("Hub");
    }

    public bool IsLevelCompleted(string Kingdom)
    {
        // Check if the level is completed
        return PlayerPrefs.GetInt(Kingdom + "_Completed", 0) == 1;
    }

}
