using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject wall;
    public GameObject coinPrefab;
    public Transform coinSpawn;
    bool countdownStarted = false;
    [SerializeField] TextMeshProUGUI timerText;
    bool coinSpawned = false;
    [SerializeField] float remainingTime;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        wall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(countdownStarted)
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
                gameManager.CancelInvoke("SpawnEnemy");
                Instantiate(coinPrefab, coinSpawn.position, Quaternion.identity);
                coinSpawned = true; // Set the flag to true so that coin is spawned only once
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !countdownStarted)
        {
            countdownStarted = true;
            wall.SetActive(true);
        }
    }

}
