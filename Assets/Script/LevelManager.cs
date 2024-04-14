using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
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