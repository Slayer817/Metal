using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProperties : MonoBehaviour
{
    public string weaponName;
    public string fireMode;
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

    [Header("Fully Automatic Setings")]
    public bool isFullyAutomatic; public float timeBetweenFABullets = .01f;

    [Header("Burst Mode Settings")]
    public bool isBurstWeapon; public float timeBetweenBurstBullets = .01f, timeBetweenBurstCompletion = .01f;

    [Header("Single Fire Settings")]
    public bool isSingleFire; public float timeBetweenSingleBullets = .01f;

    public bool usesMags;
    public bool usesShells;
    public bool usesSingleAmmo;

    [Header("What kind of single ammo? (If its bullets, leave unchecked)")]
    public bool usesGrenades;
    public bool usesRockets;

}
