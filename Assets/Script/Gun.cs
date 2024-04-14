using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{

     //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;

    // Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public CameraShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;
    public TextMeshProUGUI MiniText;
    Target targetScript;
    PlayerMovement pMovement;
    GameObject player;
    AudioManager audioManager;

    private void Awake()
    {
        player = GameObject.Find("Player");
        bulletsLeft = magazineSize;
        readyToShoot = true;
        pMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void Start()
    {

    }

    private void Update()
    {

        MyInput();

        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
        MiniText.text = magazineSize.ToString();
    }

private void MyInput()
{
    // Get input from the right trigger (RT) and the left trigger (LT)
    float rightTrigger = Input.GetAxis("RightTrigger");
    float leftTrigger = Input.GetAxis("LeftTrigger");

    if (allowButtonHold)
        {
            shooting = Input.GetButton("RT");
        }
    else
    {
        shooting = Input.GetButtonDown("RT");
    }
    if (Input.GetButtonDown("SwitchMags") && bulletsLeft < magazineSize && !reloading)
        Reload();

    // Check if either trigger is being pressed
    if (readyToShoot && (shooting || rightTrigger > 0) && !reloading && bulletsLeft > 0)
    {
        bulletsShot = bulletsPerTap;
        Shoot();
        ShootAnim();
    }
}
 
    private void OnShoot()
    {
        Debug.Log("shootValue");
        Shoot();
    }
void Shoot()
    {
        readyToShoot = false;
        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            Target enemyHealth = rayHit.collider.GetComponent<Target>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

        }

        //ShakeCamera


        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        bulletsLeft--;
        bulletsShot--;


        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        audioManager.PlaySFX(audioManager.Bullet);
    }

    public void ShootAnim()
    {
         pMovement.Shooting();
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        pMovement.Reloading();
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
