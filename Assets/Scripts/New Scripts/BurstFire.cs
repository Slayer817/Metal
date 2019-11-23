using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFire : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerController pController;
    public PlayerInventoryManager pInventory;
    public WeaponProperties wProperties;
    public GeneralWeapProperties gwProperties;

    public Animator anim;

    private bool burstEnabler = true;
    private int burstCounter = 3;
    public float burstInterval = 0.5f;
    private bool isShooting;

    public bool ThisisShooting = false;

    public bool hasFoundComponents = false;

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

        wProperties.currentAmmo -= 1;

        if (anim != null)
        {
            anim.Play("Fire", 0, 0f);
        }

        //If random muzzle is false
        if (!gwProperties.randomMuzzleflash &&
            gwProperties.enableMuzzleflash == true /*&& !silencer*/)
        {
            //gwProperties.muzzleParticles.Emit(1);
            //Light flash start
            //StartCoroutine(gwProperties.muzzleflashlight());
        }
        else if (gwProperties.randomMuzzleflash == true)
        {
            //Only emit if random value is 1
            if (gwProperties.randomMuzzleflashValue == 1)
            {
                if (gwProperties.enableSparks == true)
                {
                    //Emit random amount of spark particles
                    gwProperties.sparkParticles.Emit(Random.Range(gwProperties.minSparkEmission, gwProperties.maxSparkEmission));
                }
                if (gwProperties.enableMuzzleflash == true /*&& !silencer*/)
                {
                    gwProperties.muzzleParticles.Emit(1);
                    //Light flash start
                    //StartCoroutine(MuzzleFlashLight());
                }
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Spawn bullet from bullet spawnpoint
        var bullet = (Transform)Instantiate(gwProperties.bulletPrefab, gwProperties.bulletSpawnPoint.transform.position, gwProperties.bulletSpawnPoint.transform.rotation);

        //Add velocity to the bullet

        //bullet.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * bulletForce);

        BulletDetector detectorScript = bullet.GetComponent<BulletDetector>();
        

        //Spawn casing prefab at spawnpoint
        Instantiate(gwProperties.bigCasingPrefab,
            gwProperties.casingSpawnPoint.transform.position,
            gwProperties.casingSpawnPoint.transform.rotation);
        Debug.Log("Spawned Bullet");

    }

    public void Update()
    {
        if (pController.isShooting && burstEnabler)
        {
            StartCoroutine(Burst());
        }

        /*if (Input.GetMouseButtonUp(0))
        {
            pController.isShooting = false;
        }*/

        if (pInventory.activeWeapIs == 0)
        {
            wProperties = pInventory.weaponEquiped[0].gameObject.GetComponent<WeaponProperties>();
            anim = pInventory.weaponEquiped[0].gameObject.GetComponent<Animator>();
        }

        else if (pInventory.activeWeapIs == 1)
        {
            wProperties = pInventory.weaponEquiped[1].gameObject.GetComponent<WeaponProperties>();
            anim = pInventory.weaponEquiped[1].gameObject.GetComponent<Animator>();
        }
    }



        IEnumerator Burst()
        {
            burstEnabler = false;
            ThisisShooting = true;

            for (int i = 0; i < burstCounter; i++)
            {
                Start();
                


                Debug.Log(burstCounter);
                        

                yield return new WaitForSeconds(burstInterval);

            }

            isShooting = false;
            yield return new WaitForSeconds(burstInterval);



        }
    


}
