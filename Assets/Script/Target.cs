using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float currentHealth;
    public Collider[] hitBoxes;

    PlayerMovement playerScript;
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
