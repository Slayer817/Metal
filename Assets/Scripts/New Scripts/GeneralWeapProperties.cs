using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralWeapProperties : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerInventoryManager pInventory;

    public bool hasFoundComponents = false;

    public GameObject currentActiveWeapon;
    public float grenadeSpawnDelay = 0.35f;

    public float showBulletInMagDelay = 0.6f;
    public SkinnedMeshRenderer bulletInMagRenderer;
    
    
    public bool randomMuzzleflash = true;
    public int minRandomValue = 1;    
    public int maxRandomValue = 5;
    public int randomMuzzleflashValue;
    public bool enableMuzzleflash = true;
    public ParticleSystem muzzleParticles;

    public bool enableSparks = true;
    public ParticleSystem sparkParticles;
    public int minSparkEmission = 1;
    public int maxSparkEmission = 7;
    
    public Light muzzleflashLight;
    public float lightDuration = 0.02f;
    
    
    [Header("Prefabs")]
    public Transform bulletPrefab;
    public Transform bigCasingPrefab;
    public Transform smallCasingPrefab;
    public Transform shotgunShellPrefab;
    public Transform grenadePrefab;
    public Transform grenadeLauncherProjectilePrefab;
    public Transform rocketProjectilePrefab;


    [Header("Spawnpoints")]
    public Transform casingSpawnPoint;
    public Transform bulletSpawnPoint;
    public Transform grenadeSpawnPoint;


    

    void Start()
    {

        if(hasFoundComponents == false)
        {
            
            pInventory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<PlayerInventoryManager>();

            
            muzzleParticles = GameObject.FindGameObjectWithTag("Muzzleflash Particles").GetComponent<ParticleSystem>();
            sparkParticles = GameObject.FindGameObjectWithTag("Spark Particles").GetComponent<ParticleSystem>();
            muzzleflashLight = GameObject.FindGameObjectWithTag("Muzzleflash Light").GetComponent<Light>();

            muzzleflashLight.enabled = false;


            //Prefabs.bulletPrefab = (GameObject)Resources.Load("Bullet_Prefab", typeof(GameObject));

            casingSpawnPoint = GameObject.FindGameObjectWithTag("Casing Spawn Point").GetComponent<Transform>();
            bulletSpawnPoint = GameObject.FindGameObjectWithTag("Bullet Spawn Point").GetComponent<Transform>();
            grenadeSpawnPoint = GameObject.FindGameObjectWithTag("Grenade Spawn Point").GetComponent<Transform>();

            hasFoundComponents = true;
        }
        
        bulletInMagRenderer = GameObject.FindGameObjectWithTag("Bullet Renderer").GetComponent<SkinnedMeshRenderer>(); //The Mesh Renderer is different for every weapon because its the bullet inside the mag
                
    }

    private void Update()
    {
        if (pInventory.activeWeapIs == 0)
        {
            currentActiveWeapon = pInventory.weaponEquiped[0];
        }
        else if (pInventory.activeWeapIs == 1 && pInventory.weaponEquiped[1] != null)
        {
            currentActiveWeapon = pInventory.weaponEquiped[1];
        }
    }

    //Enable bullet in mag renderer after set amount of time
    public IEnumerator ShowBulletInMag()
    {

        //Wait set amount of time before showing bullet in mag
        yield return new WaitForSeconds(showBulletInMagDelay);
        bulletInMagRenderer.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

    //Show light when shooting, then disable after set amount of time
    public IEnumerator MuzzleFlashLight()
    {

        muzzleflashLight.enabled = true;
        yield return new WaitForSeconds(lightDuration);
        muzzleflashLight.enabled = false;
    }

}
