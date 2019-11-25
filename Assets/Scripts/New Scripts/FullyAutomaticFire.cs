using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullyAutomaticFire : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerController pController;
    public PlayerInventoryManager pInventory;
    public WeaponProperties wProperties;
    public GeneralWeapProperties gwProperties;

    public Animator anim;
    
    public float nextFireInterval;

    private bool ThisisShooting = false;
    private bool hasButtonDown = false;

    private bool hasFoundComponents = false;

    public void Start()
    {
        if (hasFoundComponents == false)
        {
            pController = GetComponent<PlayerController>();
            pInventory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<PlayerInventoryManager>();
            wProperties = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponProperties>();
            gwProperties = GameObject.FindGameObjectWithTag("Player").GetComponent<GeneralWeapProperties>();

            hasFoundComponents = true;

        }



        if (ThisisShooting && wProperties.isFullyAutomatic)
        {

            wProperties.currentAmmo -= 1;

            if (pController.anim != null)
            {
                pController.anim.Play("Fire", 0, 0f);
            }

            //If random muzzle is false
            if (!gwProperties.randomMuzzleflash &&
                gwProperties.enableMuzzleflash == true /*&& !silencer*/)
            {
                gwProperties.muzzleParticles.Emit(1);
                //Light flash start
                StartCoroutine(gwProperties.MuzzleFlashLight());
            }
            else if (gwProperties.randomMuzzleflash == true)
            {
                Debug.Log("In Random Muzzle Flash");
                //Only emit if random value is 1
                if (gwProperties.randomMuzzleflashValue == 1)
                {
                    if (gwProperties.enableSparks == true)
                    {
                        Debug.Log("Emitted Random Spark");
                        //Emit random amount of spark particles
                        gwProperties.sparkParticles.Emit(Random.Range(gwProperties.minSparkEmission, gwProperties.maxSparkEmission));

                    }
                    if (gwProperties.enableMuzzleflash == true /*&& !silencer*/)
                    {
                        Debug.Log("Coroutine Muzzle Flashlight");
                        gwProperties.muzzleParticles.Emit(1);
                        //Light flash start
                        StartCoroutine(gwProperties.MuzzleFlashLight());


                    }
                }
            }


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Spawn bullet from bullet spawnpoint
            var bullet = (Transform)Instantiate(gwProperties.bulletPrefab, gwProperties.bulletSpawnPoint.transform.position, gwProperties.bulletSpawnPoint.transform.rotation);

            BulletDetector detectorScript = bullet.GetComponent<BulletDetector>();

            //Spawn casing prefab at spawnpoint
            Instantiate(gwProperties.bigCasingPrefab, gwProperties.casingSpawnPoint.transform.position, gwProperties.casingSpawnPoint.transform.rotation);

        }

    }

    public void Update()
    {

        nextFireInterval = wProperties.timeBetweenFABullets;

        if (pController.isShooting && !ThisisShooting)
        {
            StartCoroutine(Fire());
        }


        if (pInventory.activeWeapIs == 0)
        {
            wProperties = pInventory.weaponEquiped[0].gameObject.GetComponent<WeaponProperties>();
        }

        else if (pInventory.activeWeapIs == 1)
        {
            wProperties = pInventory.weaponEquiped[1].gameObject.GetComponent<WeaponProperties>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            hasButtonDown = false;
        }

    }





    IEnumerator Fire()
    {
        ThisisShooting = true;

        Start();

        yield return new WaitForSeconds(nextFireInterval);
        ThisisShooting = false;
               
    }
}
