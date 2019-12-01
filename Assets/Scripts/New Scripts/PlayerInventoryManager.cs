using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    [Header("Other Scripts")]
    public SFXManager sfxManager;
    public CrosshairScript crosshairScript;
    public ControllerScript cScript;

    [Space(20)]
    [Header("Data")]
    public string StartingWeapon = "M4";
    public int activeWeapIs = 0;
    public bool hasSecWeap = false;

    [Space(20)]
    [Header("Equipped Weapons")]
    public GameObject[] weaponEquiped = new GameObject[2];

    [Space(20)]
    [Header("Unequipped Weapons")]
    public GameObject[] Unequipped = new GameObject[25];

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    
    public void Start()
    {
        FindComponents();
        FindAllWeaponsInPlayer();
        EquipStartingWeapon();

        cScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerScript>();

        sfxManager.mainAudioSource.clip = sfxManager.cockingClip1;
        sfxManager.mainAudioSource.Play();

    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0 || cScript.SwitchWeaponsButtonPressed)
        {
            SwapWeapons();

        }



    }

    public void SwapWeapons()
    {
        if (hasSecWeap == true)
        {
            if (weaponEquiped[0].gameObject.activeSelf)
            {
                weaponEquiped[1].gameObject.SetActive(true);
                weaponEquiped[0].gameObject.SetActive(false);

                sfxManager.mainAudioSource.clip = sfxManager.cockingClip1;
                sfxManager.mainAudioSource.Play();

                activeWeapIs = 1;
            }

            else if (weaponEquiped[1].gameObject.activeSelf)
            {
                weaponEquiped[1].gameObject.SetActive(false);
                weaponEquiped[0].gameObject.SetActive(true);

                sfxManager.mainAudioSource.clip = sfxManager.cockingClip2;
                sfxManager.mainAudioSource.Play();

                activeWeapIs = 0;
            }
        }
    }

    void FindComponents()
    {
        sfxManager = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>();
        crosshairScript = GameObject.FindGameObjectWithTag("Crosshairs").GetComponent<CrosshairScript>();
    }

    void FindAllWeaponsInPlayer()
    {
        int childCounter = 0;



        foreach (Transform child in transform)
        {
            if (child.tag == "Weapon Binder")
            {
                foreach (Transform childOFchild in child)
                {
                    if (childOFchild.tag == "Weapon")
                    {
                        if (childOFchild.gameObject.GetComponent<WeaponProperties>() != null)
                        {
                            Unequipped[childCounter] = childOFchild.gameObject;
                            Unequipped[childCounter].gameObject.GetComponent<WeaponProperties>().weaponName = Unequipped[childCounter].gameObject.name;
                            Unequipped[childCounter].gameObject.GetComponent<WeaponProperties>().storedWeaponNumber = childCounter;
                            childCounter = childCounter + 1;
                        }


                    }

                }
            }


        }
    }

    void EquipStartingWeapon()
    {
        for (int i = 0; i < Unequipped.Length; i++)
        {

            if (Unequipped[i] != null)
            {
                if (Unequipped[i].name == StartingWeapon)
                {
                    weaponEquiped[0] = Unequipped[i].gameObject;
                    Unequipped[i] = null;
                }

                else if (Unequipped[i].name != StartingWeapon)
                {
                    Unequipped[i].gameObject.SetActive(false);
                }
            }
        }
    }

    
}
