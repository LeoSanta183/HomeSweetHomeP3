using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public int selectedWeapon = 0;
    public GameObject GunUI;
    public GameObject MiniGunUI;
    public GameObject MeleeUI;
    public GameObject MiniMeleeUI;

    void Start()
    {
        SelectWeapon();
        MeleeUI.SetActive(false);
        MiniGunUI.SetActive(false);
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        // Weapon swapping using the mouse scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            SelectNextWeapon();

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            SelectPreviousWeapon();

        // Weapon swapping using the number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            selectedWeapon = 1;

        // Weapon swapping using the Y button on the Xbox controller
        if (Input.GetButtonDown("YButton"))
        {
            // Toggle between weapons if there are multiple weapons available
            if (transform.childCount >= 2)
                selectedWeapon = (selectedWeapon + 1) % transform.childCount;
        }

        // Update weapon UI and selection
        if (previousSelectedWeapon != selectedWeapon)
            SelectWeapon();
    }

    void SelectNextWeapon()
    {
        selectedWeapon = (selectedWeapon + 1) % transform.childCount;
        SelectWeapon(); // Update UI when selecting next weapon
    }

    void SelectPreviousWeapon()
    {
        selectedWeapon = (selectedWeapon - 1 + transform.childCount) % transform.childCount;
        SelectWeapon(); // Update UI when selecting previous weapon
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            bool isActive = i == selectedWeapon;
            weapon.gameObject.SetActive(isActive);

            // Update UI based on weapon selection
            if (isActive)
            {
                if (i == 0)
                {
                    GunUI.SetActive(true);
                    MiniGunUI.SetActive(false);
                    MeleeUI.SetActive(false);
                    MiniMeleeUI.SetActive(true);
                }
                else
                {
                    GunUI.SetActive(false);
                    MiniGunUI.SetActive(true);
                    MeleeUI.SetActive(true);
                    MiniMeleeUI.SetActive(false);
                }
            }

            i++;
        }
    }
}