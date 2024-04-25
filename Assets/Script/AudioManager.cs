using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip EnemyHit; 
    public AudioClip SwitchWeapon;
    public AudioClip Bullet;
    public AudioClip CoinPickUp;
    public AudioClip MeleeSlash;
    public AudioClip Footsteps;
    public AudioClip buttonHover;
    public AudioClip Grapple;
    public AudioClip CarrotGrunt;
    public AudioClip BroccoliGrunt;
    public AudioClip RockCollapse;
    public AudioClip reloadMag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
