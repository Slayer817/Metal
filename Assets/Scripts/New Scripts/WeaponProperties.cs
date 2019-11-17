using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProperties : MonoBehaviour
{
    public string weaponName;
    public string WeaponType;
    public int storedWeaponNumber;

    public int damage = 50;
    public int bulletSpeed = 250;

    //How much ammo is currently left
    public int currentAmmo;
    //Totalt amount of ammo
    [Tooltip("How much ammo the weapon should have.")]
    public int ammo;
    //Check if out of ammo
    public bool outOfAmmo;

}
