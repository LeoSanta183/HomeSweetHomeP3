using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Gun gun;
    public Toggle hardMode;
    public Toggle invincible;
    public Toggle infiniteAmmo;
    public TextMeshProUGUI toggleText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OneHealth()
    {
        playerHealth.VeteranMode();
        toggleText.gameObject.SetActive(true);
        toggleText.text = "One Health!";
    }

    public void ZeroDamage()
    {
        playerHealth.MakeInvincible();
        toggleText.gameObject.SetActive(true);
        toggleText.text = "Invincibiltiy!";
    }

    public void SprayAndPray()
    {
        gun.InfiniteShoot();
        toggleText.gameObject.SetActive(true);
        toggleText.text = "SPRAY AND PRAY!";
    }
}

