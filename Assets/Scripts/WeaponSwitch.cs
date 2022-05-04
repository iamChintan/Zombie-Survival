using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private int selectedWeapon = 0;
    string selectedWeaponName = "";
    [SerializeField] private Gun sniperGun;

    void Start()
    {
        selectWeapon();
    }

    private void selectWeapon()
    {
        int i = 0;
        foreach (Transform item in transform)
        {
            if (i == selectedWeapon)
            {
                item.gameObject.SetActive(true);
            }
            else
                item.gameObject.SetActive(false);
            i++;
        }
    }

    void Update()
    {
        int previousSelected = selectedWeapon;
        if(!sniperGun.isZoomed)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }

        }

        selectedWeaponName = transform.GetChild(previousSelected).name;;

        if (previousSelected != selectedWeapon)
        {
            selectWeapon();
        }
    }
}
