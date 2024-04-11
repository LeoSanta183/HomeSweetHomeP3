using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform coinSpawn;
    [SerializeField] TextMeshProUGUI timerText;
    bool coinSpawned = false;
    [SerializeField] float remainingTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }
        else if (remainingTime <= 0 && !coinSpawned)
        {
            Instantiate(coinPrefab, coinSpawn.position, Quaternion.identity);
            coinSpawned = true; // Set the flag to true so that coin is spawned only once
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
    }
}
