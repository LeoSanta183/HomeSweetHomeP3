using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float health;
    
    private float lerpSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        
        if(healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }
    }

    public void TakeDamage (float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            SceneManager.LoadScene("Lose Scene");
            Debug.Log("You lose");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(50);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(50);
        }
    }
    
}
