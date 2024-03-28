using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float currentHealth;
    public Collider[] hitBoxes;

    void Start()
    {
        currentHealth = health;
    }
    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
