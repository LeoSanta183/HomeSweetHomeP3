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
    public GameObject lossScreen;
    public bool isInvincible;
    public bool youAreCooked;
    
    
    private float lerpSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        lossScreen.SetActive(false);
        Time.timeScale = 1;
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
    public void MakeInvincible()
    {
        isInvincible = true;
    }

    public void VeteranMode()
    {
        youAreCooked = true;
    }

    public void TakeDamage (float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            lossScreen.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isInvincible && !youAreCooked)
        {
            TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible && !youAreCooked)
        {
            TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("Bullet") && !isInvincible && youAreCooked)
        {
            TakeDamage(100);
        }
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible && youAreCooked)
        {
            TakeDamage(100);
        }
    }
    
}
