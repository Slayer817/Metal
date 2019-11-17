using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerInventoryManager pInventory;

    [Space(20)]
    [Header("Crosshairs")]
    public GameObject ARCrosshair;
    public GameObject SMGCrosshair;
    public GameObject PistolCrosshair;

    public bool hasFoundComponents = false;

    public void Start()
    {
        if(!hasFoundComponents)
        {
            FindComponents();
            hasFoundComponents = true;
        }

        if (pInventory.activeWeapIs == 0)
        {
            if (pInventory.weaponEquiped[0].gameObject.GetComponent<WeaponProperties>().WeaponType == "Burst Weapon")
            {
                ARCrosshair.SetActive(true);
            }

            else if (pInventory.weaponEquiped[0].gameObject.GetComponent<WeaponProperties>().WeaponType == "SMG")
            {
                SMGCrosshair.SetActive(true);
            }

            else if (pInventory.weaponEquiped[0].gameObject.GetComponent<WeaponProperties>().WeaponType == "Pistol")
            {
                PistolCrosshair.SetActive(true);
            }
        }


        if (pInventory.activeWeapIs == 1)
        {
            if (pInventory.weaponEquiped[1].gameObject.GetComponent<WeaponProperties>().WeaponType == "Burst Weapon")
            {
                ARCrosshair.SetActive(true);
            }

            else if (pInventory.weaponEquiped[1].gameObject.GetComponent<WeaponProperties>().WeaponType == "SMG")
            {
                ARCrosshair.SetActive(true);
            }

            else if (pInventory.weaponEquiped[1].gameObject.GetComponent<WeaponProperties>().WeaponType == "Pistol")
            {
                ARCrosshair.SetActive(true);
            }
        }
    }

    

    public void FindComponents()
    {
        //Debug.Log(pInventory.activeWeapIs);
        pInventory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<PlayerInventoryManager>();
    }

    
}
