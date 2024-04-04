using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public int selectedWeapon = 0;
    public GameObject GunUI;
    public GameObject MiniGunUI;
    public GameObject MeleeUI;
    public GameObject MiniMeleeUI;
    // Start is called before the first frame update
    void Start()
    {
      SelectWeapon();
      MeleeUI.SetActive(false);
      MiniGunUI.SetActive(false);  
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeapon >= transform.childCount - 1)
            
            {
                selectedWeapon = 0;
                GunUI.SetActive(true);
                MiniGunUI.SetActive(false);
                MeleeUI.SetActive(false);
                MiniMeleeUI.SetActive(true);
            }
            else
            {
                selectedWeapon++;
                selectedWeapon = 1;
                GunUI.SetActive(false);
                MiniGunUI.SetActive(true);
                MeleeUI.SetActive(true);
                MiniMeleeUI.SetActive(false);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(selectedWeapon <= 0)
            {
            selectedWeapon = transform.childCount - 1;
            selectedWeapon = 1;
            GunUI.SetActive(false);
            MiniGunUI.SetActive(true);
            MeleeUI.SetActive(true);
            MiniMeleeUI.SetActive(false);
            }
            else
            {
                selectedWeapon--;
                 GunUI.SetActive(true);
                MiniGunUI.SetActive(false);
                MeleeUI.SetActive(false);
                MiniMeleeUI.SetActive(true);
            }
        }
        
        if (Input.GetKeyDown("1"))
        {
            selectedWeapon = 0;
            GunUI.SetActive(true);
            MiniGunUI.SetActive(false);
            MeleeUI.SetActive(false);
            MiniMeleeUI.SetActive(true);
        }
        
        if (Input.GetKeyDown("2") && transform.childCount >= 2)
        {
            selectedWeapon = 1;
            GunUI.SetActive(false);
            MiniGunUI.SetActive(true);
            MeleeUI.SetActive(true);
            MiniMeleeUI.SetActive(false);
        }
         
        
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selectedWeapon)
                    weapon.gameObject.SetActive(true);
                else
                    weapon.gameObject.SetActive(false);
                    i++;
            }
        }
}
