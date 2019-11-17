using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralWeapProperties : MonoBehaviour
{
    public float grenadeSpawnDelay = 0.35f;

    public float showBulletInMagDelay = 0.6f;
    public SkinnedMeshRenderer bulletInMagRenderer;
    
    
    public bool randomMuzzleflash = false;
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
    public Transform casingPrefab;
    public Transform grenadePrefab;
    
    
    [Header("Spawnpoints")]
    public Transform casingSpawnPoint;
    public Transform bulletSpawnPoint;
    public Transform grenadeSpawnPoint;


    

    void Start()
    {

    }

    void FindComponents()
    {
        bulletInMagRenderer = GameObject.FindGameObjectWithTag("Bullet Renderer").GetComponent<SkinnedMeshRenderer>();
        muzzleParticles = GameObject.FindGameObjectWithTag("Muzzleflash Particles").GetComponent<ParticleSystem>();
        sparkParticles = GameObject.FindGameObjectWithTag("Spark Particles").GetComponent<ParticleSystem>();
        muzzleflashLight = GameObject.FindGameObjectWithTag("Muzzleflash Light").GetComponent<Light>();

        muzzleflashLight.enabled = false;
        

        //Prefabs.bulletPrefab = (GameObject)Resources.Load("Bullet_Prefab", typeof(GameObject));

        casingSpawnPoint = GameObject.FindGameObjectWithTag("Casing Spawn Point").GetComponent<Transform>();
        bulletSpawnPoint = GameObject.FindGameObjectWithTag("Bullet Spawn Point").GetComponent<Transform>();
        grenadeSpawnPoint = GameObject.FindGameObjectWithTag("Grenade Spawn Point").GetComponent<Transform>();
    }
}
