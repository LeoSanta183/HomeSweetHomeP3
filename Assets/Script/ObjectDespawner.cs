using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDespawner : MonoBehaviour
{
    public GameObject objectToDestroyWhenQuestComplete;
    public int totalEnemiesRequired = 2;
    private int currentEnemyCount = 0;

    void Start()
    {
        
    }

   
    public void EnemyDestroyed()
    {
        
        currentEnemyCount++;

        
        if (currentEnemyCount >= totalEnemiesRequired)
        {
            
            if (objectToDestroyWhenQuestComplete != null)
            {
                Destroy(objectToDestroyWhenQuestComplete);
            }
            else
            {
                Debug.LogWarning("Object to destroy when quest complete is not set!");
            }
        }
    }
}
