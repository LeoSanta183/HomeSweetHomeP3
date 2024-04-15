using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDespawner : MonoBehaviour
{
    public GameObject objectToDestroyWhenQuestComplete;
    public int totalEnemiesRequired = 2;
    private int currentEnemyCount = 0;
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

   
    public void EnemyDestroyed()
    {
        
        currentEnemyCount++;

        
        if (currentEnemyCount >= totalEnemiesRequired)
        {
            
            if (objectToDestroyWhenQuestComplete != null)
            {
                Destroy(objectToDestroyWhenQuestComplete);
                audioManager.PlaySFX(audioManager.RockCollapse);
            }
            else
            {
                Debug.LogWarning("Object to destroy when quest complete is not set!");
            }
        }
    }
}