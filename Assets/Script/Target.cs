using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Collider[] hitBoxes;
    AudioManager audioManager;
    void Start()
    {
        currentHealth = maxHealth;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Perform any death-related actions here, such as playing death animation, scoring points, etc.
        Destroy(gameObject);
        audioManager.PlaySFX(audioManager.EnemyHit);
    }
}
