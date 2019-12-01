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
    public GeneralWeapProperties gwProperties;
    public WeaponProperties wProperties;
    public Animator anim;
    public FullyAutomaticFire fullyAutomaticFire;
    public BurstFire burstFire;
    public SingleFire singleFire;

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
            gwProperties = GameObject.FindGameObjectWithTag("Player").GetComponent<GeneralWeapProperties>();
            wProperties = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponProperties>();

            fullyAutomaticFire = GetComponent<FullyAutomaticFire>();
            burstFire = GetComponent<BurstFire>();
            singleFire = GetComponent<SingleFire>();
                        
        }

        
    }


    private void Update()
    {
        if(pInventory.activeWeapIs == 0)
        {
            wProperties = pInventory.weaponEquiped[0].gameObject.GetComponent<WeaponProperties>();
            anim = pInventory.weaponEquiped[0].gameObject.GetComponent<Animator>();
        }

        else if (pInventory.activeWeapIs == 1)
        {
            wProperties = pInventory.weaponEquiped[1].gameObject.GetComponent<WeaponProperties>();
            anim = pInventory.weaponEquiped[1].gameObject.GetComponent<Animator>();
        }

        // Firing
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (Input.GetMouseButton(0) && !wProperties.outOfAmmo && !isReloading && !isShooting && !isInspecting /*&& !isRunning && burstEnabler*/)
        { 
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        

        //Aiming
        //Toggle camera FOV when right click is held down
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetButton("Fire2") && !isReloading && !isRunning && !isInspecting)
        {

            isAiming = true;

            //anim.SetBool("Aim", true);

            sfxManager.mainAudioSource.clip = sfxManager.aimingSound;
            sfxManager.mainAudioSource.Play();
            

        }
        else if (Input.GetMouseButtonUp(1) && !isReloading && !isRunning && !isInspecting)
        {
            
            isAiming = false;

            //anim.SetBool("Aim", false);

            sfxManager.mainAudioSource.clip = sfxManager.aimingSound;
            sfxManager.mainAudioSource.Play();
            

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //If randomize muzzleflash is true, genereate random int values
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        if (gwProperties.randomMuzzleflash == true)
        {
            gwProperties.randomMuzzleflashValue = Random.Range(gwProperties.minRandomValue, gwProperties.maxRandomValue);
        }


        //Set current ammo text from ammo int
        playerProperties.currentAmmoText.text = wProperties.currentAmmo.ToString();

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
        if (wProperties.currentAmmo == 0)
        {
            //Show out of ammo text
            //currentWeaponText.text = "OUT OF AMMO";
            //Toggle bool
            wProperties.outOfAmmo = true;
            //Auto reload if true
            if (!isReloading)
            {
                StartCoroutine(AutoReload());
            }
        }
        else
        {

            //Toggle bool
            wProperties.outOfAmmo = false;
            //anim.SetBool ("Out Of Ammo", false);
        }
        


        //Toggle weapon holster when Z key is pressed
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


        //Inspect weapon when T key is pressed
        ///////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.Play("Reload Open", 0, 0f);
            //anim.SetTrigger("Inspect");
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
    public void Reload()
    {
        if (wProperties.usesMags)
        {
            wProperties.mainAudioSource.clip = wProperties.Reload_1;
            wProperties.mainAudioSource.Play();

            if (wProperties.outOfAmmo == true)
            {
                //Play diff anim if out of ammo
                anim.Play("Reload Out Of Ammo", 0, 0f);

                sfxManager.mainAudioSource.clip = weapSounds.reloadSoundOutOfAmmo;
                sfxManager.mainAudioSource.Play();

                //If out of ammo, hide the bullet renderer in the mag
                //Do not show if bullet renderer is not assigned in inspector
                if (gwProperties.bulletInMagRenderer != null)
                {
                    gwProperties.bulletInMagRenderer.GetComponent
                    <SkinnedMeshRenderer>().enabled = false;
                    //Start show bullet delay
                    StartCoroutine(gwProperties.ShowBulletInMag());
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
                if (gwProperties.bulletInMagRenderer != null)
                {
                    gwProperties.bulletInMagRenderer.GetComponent
                    <SkinnedMeshRenderer>().enabled = true;
                }
            }
            //Restore ammo when reloading
            wProperties.currentAmmo = wProperties.ammo;
            wProperties.outOfAmmo = false;
        }

        if(wProperties.usesShells)
        {
            if (wProperties.outOfAmmo == true)
            {
                //Play diff anim if out of ammo
                anim.Play("Reload Open", 0, 0f);
            }
            else
            {
                //Play diff anim if out of ammo
                anim.Play("Reload Open", 0, 0f);
                anim.Play("Reload Open", 0, 0f);
                Debug.Log("Reloading");
                
            }
            //Restore ammo when reloading
            wProperties.currentAmmo = wProperties.ammo;
            wProperties.outOfAmmo = false;
        }
    }

    //Check current animation playing
    private void AnimationCheck()
    {
        if (wProperties.usesMags)
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

        if(wProperties.usesShells)
        {
            //Check if reloading
            //Check both animations
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reload Open") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("Reload Open") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("Inser Shell") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("Reload Close"))
            {
                isReloading = true;
            }
            else
            {
                isReloading = false;
            }

            //Check if inspecting weapon
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Inspect"))
            {
                isInspecting = true;
            }
            else
            {
                isInspecting = false;
            }
            /*
            //Check if shooting
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
            {
                isShooting = true;
            }
            else
            {
                isShooting = false;
            }
            */
        }
    }

    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////////// Coroutines ////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <returns></returns>

    private IEnumerator GrenadeSpawnDelay()
    {

        //Wait for set amount of time before spawning grenade
        yield return new WaitForSeconds(gwProperties.grenadeSpawnDelay);
        //Spawn grenade prefab at spawnpoint
        Instantiate(gwProperties.grenadePrefab,
            gwProperties.grenadeSpawnPoint.transform.position,
            gwProperties.grenadeSpawnPoint.transform.rotation);
    }

    private IEnumerator AutoReload()
    {
        //Wait set amount of time
        yield return new WaitForSeconds(autoReloadDelay);

        if (wProperties.usesMags)
        {

            if (wProperties.outOfAmmo == true)
            {
                //Play diff anim if out of ammo
                anim.Play("Reload Out Of Ammo", 0, 0f);

                wProperties.mainAudioSource.clip = wProperties.Reload_2;
                wProperties.mainAudioSource.Play();

                //If out of ammo, hide the bullet renderer in the mag
                //Do not show if bullet renderer is not assigned in inspector
                if (gwProperties.bulletInMagRenderer != null)
                {
                    gwProperties.bulletInMagRenderer.GetComponent
                    <SkinnedMeshRenderer>().enabled = false;
                    //Start show bullet delay
                    StartCoroutine(gwProperties.ShowBulletInMag());
                }
            }
            //Restore ammo when reloading
            wProperties.currentAmmo = wProperties.ammo;
            wProperties.outOfAmmo = false;
        }

        if(wProperties.usesShells)
        {
            if (wProperties.outOfAmmo == true)
            {
                //Play diff anim if out of ammo
                anim.Play("Reload Open", 0, 0f);

                while(wProperties.currentAmmo < wProperties.ammo)
                {
                    anim.Play("Insert Shell", 0, 0f);
                    wProperties.currentAmmo += 1;
                    Debug.Log(wProperties.currentAmmo);
                }

                anim.Play("Reload Close", 0, 0f);
            }
        }
            
        if(wProperties.usesSingleAmmo)
        {
            if (wProperties.outOfAmmo == true)
            {
                //Play diff anim if out of ammo
                anim.Play("Reload", 0, 0f);
                

            }
            //Restore ammo when reloading
            wProperties.currentAmmo = wProperties.ammo;
            wProperties.outOfAmmo = false;
        }
        
    }

    
    /*
    //Enable bullet in mag renderer after set amount of time
    private IEnumerator ShowBulletInMag()
    {

        //Wait set amount of time before showing bullet in mag
        yield return new WaitForSeconds(gwProperties.showBulletInMagDelay);
        gwProperties.bulletInMagRenderer.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

    //Show light when shooting, then disable after set amount of time
    private IEnumerator MuzzleFlashLight()
    {

        gwProperties.muzzleflashLight.enabled = true;
        yield return new WaitForSeconds(gwProperties.lightDuration);
        gwProperties.muzzleflashLight.enabled = false;
    }
    */
    


}


