using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float currentHealth;
    public Collider[] hitBoxes;
    public TextMeshProUGUI text;
    PlayerMovement playerScript;
    
    AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void Start()
    
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        currentHealth = health;
        
    }
    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0f && gameObject.CompareTag("Enemy"))
        {
            audioManager.PlaySFX(audioManager.EnemyHit);
            Die();
        }
        if(currentHealth <= 0f && gameObject.CompareTag("Player"))
        {
            playerScript.Lose();
            Debug.Log("You lose");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
