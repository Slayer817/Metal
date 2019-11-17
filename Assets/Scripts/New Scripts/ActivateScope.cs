using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScope : MonoBehaviour
{
    public PlayerInventory pInventory;
    //public ScopesProperties scopeProperties;

    public GameObject BurstWeaponScopeGO;

    private void Start()
    {
        BurstWeaponScopeGO.gameObject.SetActive(false);
    }

    public void enableScope()
    {
        if (pInventory.weaponEquiped[0].gameObject.GetComponent<WeaponProperties>().WeaponType == "Burst Weapon")
        {
            BurstWeaponScopeGO.gameObject.SetActive(true);
        }
    }
}
