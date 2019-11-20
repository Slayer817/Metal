using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Other Scripts")]
    public Aiming aimingScript;
    public SFXManager sfxManager;
    public WeaponSounds weapSounds;
    public PlayerProperties playerProperties;
    public PlayerInventoryManager pInventory;
    public GeneralWeapProperties generalWeapProp;
    public WeaponProperties weapProperties;
    public Animator anim;

    [HideInInspector]
    public bool hasBeenHolstered = false, holstered, isRunning, isAiming, isWalking;
    [HideInInspector]
    public bool isInspecting, isReloading, isShooting, aimSoundHasPlayed = false, hasFoundComponents = false;

    //Used for fire rate
    private float lastFired;
    [Header("Weapon Settings")]
    //How fast the weapon fires, higher value means faster rate of fire
    [Tooltip("How fast the weapon fires, higher value means faster rate of fire.")]
    public float fireRate;
    //Eanbles auto reloading when out of ammo
    [Tooltip("Enables auto reloading when out of ammo.")]
    public bool autoReload;
    //Delay between shooting last bullet and reloading
    public float autoReloadDelay;
    //Check if reloading
    //private bool isReloading;


    public void Start()
    {
        if(hasFoundComponents == false)
        {
            aimingScript = GameObject.FindGameObjectWithTag("Scope BG").GetComponent<Aiming>();
            sfxManager = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>();
            weapSounds = GameObject.FindGameObjectWithTag("Weapon Sounds").GetComponent<WeaponSounds>();

            pInventory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<PlayerInventoryManager>();

            playerProperties = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProperties>();
            generalWeapProp = GameObject.FindGameObjectWithTag("Player").GetComponent<GeneralWeapProperties>();
            weapProperties = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponProperties>();
                        
        }

        
    }


    private void Update()
    {
        if(pInventory.activeWeapIs == 0)
                {
                    anim = pInventory.weaponEquiped[0].gameObject.GetComponent<Animator>();
                }

        else if (pInventory.activeWeapIs == 1)
        {
            anim = pInventory.weaponEquiped[1].gameObject.GetComponent<Animator>();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Aiming
        //Toggle camera FOV when right click is held down
        if (Input.GetButton("Fire2") && !isReloading && !isRunning && !isInspecting)
        {

            isAiming = true;

            aimingScript.aimingBG.SetActive(true);
              
            sfxManager.mainAudioSource.clip = sfxManager.aimingSound;
            sfxManager.mainAudioSource.Play();
            

        }
        else if (Input.GetMouseButtonUp(1) && !isReloading && !isRunning && !isInspecting)
        {
            
            isAiming = false;

            aimingScript.aimingBG.SetActive(false);
                        
            sfxManager.mainAudioSource.clip = sfxManager.aimingSound;
            sfxManager.mainAudioSource.Play();
            

        }
        //Aiming end


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //If randomize muzzleflash is true, genereate random int values
        if (generalWeapProp.randomMuzzleflash == true)
        {
            generalWeapProp.randomMuzzleflashValue = Random.Range(generalWeapProp.minRandomValue, generalWeapProp.maxRandomValue);
        }


        //Set current ammo text from ammo int
        playerProperties.currentAmmoText.text = weapProperties.currentAmmo.ToString();

        //Continosuly check which animation 
        //is currently playing
        AnimationCheck();

        //Play knife attack 1 animation when Q key is pressed
        if (Input.GetKeyDown(KeyCode.Q) /* && !isInspecting */)
        {
            anim.Play("Knife Attack 1", 0, 0f);
        }
        //Play knife attack 2 animation when F key is pressed
        if (Input.GetKeyDown(KeyCode.F) /* && !isInspecting */)
        {
            anim.Play("Knife Attack 2", 0, 0f);
        }

        //Throw grenade when pressing G key
        if (Input.GetKeyDown(KeyCode.G) /* && !isInspecting */)
        {
            StartCoroutine(GrenadeSpawnDelay());
            //Play grenade throw animation
            anim.Play("GrenadeThrow", 0, 0.0f);
        }

        //If out of ammo
        if (weapProperties.currentAmmo == 0)
        {
            //Show out of ammo text
            //currentWeaponText.text = "OUT OF AMMO";
            //Toggle bool
            weapProperties.outOfAmmo = true;
            //Auto reload if true
            if (!isReloading)
            {
                StartCoroutine(AutoReload());
            }
        }
        else
        {

            //Toggle bool
            weapProperties.outOfAmmo = false;
            //anim.SetBool ("Out Of Ammo", false);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //AUtomatic fire
        //Left click hold 
        if (Input.GetMouseButton(0) && !weapProperties.outOfAmmo && !isReloading && !isShooting && !isInspecting /*&& !isRunning && burstEnabler*/)
        {
            //StartCoroutine(Burst());
        }


        if (Input.GetMouseButtonUp(0))
        {

            //burstEnabler = true;

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("In scope");
        }


        //Toggle weapon holster when E key is pressed

        ////////////////////////////////////////////////////////////////////// Toggle weapon holster when pressing Z key
        if (Input.GetKeyDown(KeyCode.Z) && !hasBeenHolstered)
        {
            holstered = true;

            sfxManager.mainAudioSource.clip = weapSounds.holsterSound;
            sfxManager.mainAudioSource.Play();

            hasBeenHolstered = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && hasBeenHolstered)
        {
            holstered = false;

            sfxManager.mainAudioSource.clip = weapSounds.drawWeaponSound;
            sfxManager.mainAudioSource.Play();

            hasBeenHolstered = false;
        }

        //Holster anim toggle
        if (holstered == true)
        {
            anim.SetBool("Holster", true);
        }
        else
        {
            anim.SetBool("Holster", false);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Inspect weapon when T key is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetTrigger("Inspect");
        }

        //Reload 
        if (Input.GetKeyDown(KeyCode.R) && !isReloading /* && !isInspecting */)
        {
            //Reload
            Reload();
        }

        //Walking when pressing down WASD keys
        if (Input.GetKey(KeyCode.W) /* && !isRunning */ ||
            Input.GetKey(KeyCode.A) /* && !isRunning */ ||
            Input.GetKey(KeyCode.S) /* && !isRunning */ ||
            Input.GetKey(KeyCode.D) /* && !isRunning */)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }


    }

    //Reload
    private void Reload()
    {

        if (weapProperties.outOfAmmo == true)
        {
            //Play diff anim if out of ammo
            anim.Play("Reload Out Of Ammo", 0, 0f);

            sfxManager.mainAudioSource.clip = weapSounds.reloadSoundOutOfAmmo;
            sfxManager.mainAudioSource.Play();

            //If out of ammo, hide the bullet renderer in the mag
            //Do not show if bullet renderer is not assigned in inspector
            if (generalWeapProp.bulletInMagRenderer != null)
            {
                generalWeapProp.bulletInMagRenderer.GetComponent
                <SkinnedMeshRenderer>().enabled = false;
                //Start show bullet delay
                StartCoroutine(ShowBulletInMag());
            }
        }
        else
        {
            //Play diff anim if ammo left
            anim.Play("Reload Ammo Left", 0, 0f);

            sfxManager.mainAudioSource.clip = weapSounds.reloadSoundAmmoLeft;
            sfxManager.mainAudioSource.Play();

            //If reloading when ammo left, show bullet in mag
            //Do not show if bullet renderer is not assigned in inspector
            if (generalWeapProp.bulletInMagRenderer != null)
            {
                generalWeapProp.bulletInMagRenderer.GetComponent
                <SkinnedMeshRenderer>().enabled = true;
            }
        }
        //Restore ammo when reloading
        weapProperties.currentAmmo = weapProperties.ammo;
        weapProperties.outOfAmmo = false;
    }

    //Check current animation playing
    private void AnimationCheck()
    {

        //Check if reloading
        //Check both animations
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reload Out Of Ammo") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Reload Ammo Left"))
        {
            isReloading = true;
        }
        else
        {
            isReloading = false;
        }

    }

    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////////// Coroutines ////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <returns></returns>

    private IEnumerator GrenadeSpawnDelay()
    {

        //Wait for set amount of time before spawning grenade
        yield return new WaitForSeconds(generalWeapProp.grenadeSpawnDelay);
        //Spawn grenade prefab at spawnpoint
        Instantiate(generalWeapProp.grenadePrefab,
            generalWeapProp.grenadeSpawnPoint.transform.position,
            generalWeapProp.grenadeSpawnPoint.transform.rotation);
    }

    private IEnumerator AutoReload()
    {
        //Wait set amount of time
        yield return new WaitForSeconds(autoReloadDelay);

        if (weapProperties.outOfAmmo == true)
        {
            //Play diff anim if out of ammo
            anim.Play("Reload Out Of Ammo", 0, 0f);

            sfxManager.mainAudioSource.clip = weapSounds.reloadSoundOutOfAmmo;
            sfxManager.mainAudioSource.Play();

            //If out of ammo, hide the bullet renderer in the mag
            //Do not show if bullet renderer is not assigned in inspector
            if (generalWeapProp.bulletInMagRenderer != null)
            {
                generalWeapProp.bulletInMagRenderer.GetComponent
                <SkinnedMeshRenderer>().enabled = false;
                //Start show bullet delay
                StartCoroutine(ShowBulletInMag());
            }
        }
        //Restore ammo when reloading
        weapProperties.currentAmmo = weapProperties.ammo;
        weapProperties.outOfAmmo = false;
    }

    

    //Enable bullet in mag renderer after set amount of time
    private IEnumerator ShowBulletInMag()
    {

        //Wait set amount of time before showing bullet in mag
        yield return new WaitForSeconds(generalWeapProp.showBulletInMagDelay);
        generalWeapProp.bulletInMagRenderer.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

    //Show light when shooting, then disable after set amount of time
    private IEnumerator MuzzleFlashLight()
    {

        generalWeapProp.muzzleflashLight.enabled = true;
        yield return new WaitForSeconds(generalWeapProp.lightDuration);
        generalWeapProp.muzzleflashLight.enabled = false;
    }

    


}


