using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProperties : MonoBehaviour
{
    [Header("Weapon Info")]
    public string weaponName;
    public string fireMode;
    public int storedWeaponNumber;
    public int damage = 50;
    public int bulletSpeed = 250;

    [Header("Sounds")]
    public AudioClip Fire;
    public AudioClip Reload_1;
    public AudioClip Reload_2;

    [Header("Components")]
    public AudioSource mainAudioSource;

    

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

    private bool hasFoundComponents = false;

    private void Start()
    {
     
        if(hasFoundComponents == false)
        {
            mainAudioSource = GetComponent<AudioSource>();

            hasFoundComponents = true;
        }
        
    }

}
