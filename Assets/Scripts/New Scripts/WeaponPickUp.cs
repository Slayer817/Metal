using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickUp : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerInventoryManager pInventory;
    public PlayerController pController;
    public SFXManager sfxManager;
    public Text pickupText;
    
    public GameObject pickupWeap; // Stores weapon in order to use Update void without "other"
    public string weaponName;
    public int puWeapStoredNumber;
    public int equippedWeapStoredNum;


    KeyCode pickup = KeyCode.E;

    private bool isOnTrigger = false;
    private bool canPickup = false;
    private bool hasSecWeap = false;

    private void Start()
    {
        pInventory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<PlayerInventoryManager>();
        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pickupText = GameObject.FindGameObjectWithTag("Player Informer").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        isOnTrigger = true;

        if (isOnTrigger == true)
        {

            if (other.gameObject.tag == "Pickable") //Check if weapon on maps have the Pickable Tag
            {
                
                for (int i = 0; i < pInventory.Unequipped.Length; i++) //Looks for a weapon in the Unequipped Array for the Gameobject with the same name
                {
                    if (pInventory.Unequipped[i] != null)
                    {
                        if (other.gameObject.name == pInventory.Unequipped[i].gameObject.name)
                        {
                            pickupWeap = pInventory.Unequipped[i].gameObject; // Adds the weapon in "pickupWeap"
                            puWeapStoredNumber = pickupWeap.gameObject.GetComponent<WeaponProperties>().storedWeaponNumber;
                            

                            pickupText.text = "Pick up " + pickupWeap.name;
                            canPickup = true;

                            Debug.Log(canPickup);
                                                        
                        }
                        
                    }
                }                                             
            }            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickupText.text = "";
        isOnTrigger = false;
        canPickup = false;

    }

    private void Update()
    {
        if (isOnTrigger == true && canPickup == true)
        {
            if (Input.GetKeyDown(pickup))
            {                 

                if (pInventory.weaponEquiped[1] == null) // Looks for Secondary Weapon
                {
                    
                        PickupSecWeap();
                    
                }

                else // Replace Equipped weapon
                {

                    ReplaceWeapon();
                }

            }

        }
    }




    



    public void ReplaceWeapon()
    {
        //Debug.Log(weaponPUTZ.)
        Debug.Log("pickup new initialised");

        if (pInventory.activeWeapIs == 1)
        {
            
                        if (pInventory.Unequipped[puWeapStoredNumber].gameObject.GetComponent<WeaponProperties>() != null)
                        {
                            equippedWeapStoredNum = pInventory.weaponEquiped[1].gameObject.GetComponent<WeaponProperties>().storedWeaponNumber;

                            pInventory.Unequipped[puWeapStoredNumber].gameObject.SetActive(true);
                            pInventory.weaponEquiped[1].gameObject.SetActive(false);

                            sfxManager.mainAudioSource.clip = sfxManager.cockingClip1;
                            sfxManager.mainAudioSource.Play();

                            pInventory.Unequipped[equippedWeapStoredNum] = pInventory.weaponEquiped[1];
                            pInventory.weaponEquiped[1] = pInventory.Unequipped[puWeapStoredNumber];

                            pInventory.Unequipped[puWeapStoredNumber] = null;
                        }


                    
                
            

            //weaponEquiped[1] = GameObject.FindGameObjectWithTag("Player_1_" + weaponPUTZ.pickupWeap.gameObject.name);
        }

        if (pInventory.activeWeapIs == 0)
        {
            Debug.Log("pickup 0 WorksFine 1");

            if (pInventory.Unequipped[puWeapStoredNumber].gameObject.GetComponent<WeaponProperties>() != null)
            {
                equippedWeapStoredNum = pInventory.weaponEquiped[0].gameObject.GetComponent<WeaponProperties>().storedWeaponNumber;

                pInventory.Unequipped[puWeapStoredNumber].gameObject.SetActive(true);
                pInventory.weaponEquiped[0].gameObject.SetActive(false);

                sfxManager.mainAudioSource.clip = sfxManager.cockingClip1;
                sfxManager.mainAudioSource.Play();

                pInventory.Unequipped[equippedWeapStoredNum] = pInventory.weaponEquiped[0];
                pInventory.weaponEquiped[0] = pInventory.Unequipped[puWeapStoredNumber];

                pInventory.Unequipped[puWeapStoredNumber] = null;
            }

        }

    }



    public void PickupSecWeap()
    {
        
                
                    if (pickupWeap.gameObject.GetComponent<WeaponProperties>() != null)
                    {
                        pInventory.weaponEquiped[1] = pInventory.Unequipped[puWeapStoredNumber].gameObject;
                        

                        pInventory.Unequipped[puWeapStoredNumber] = null;



                        pInventory.weaponEquiped[1].gameObject.SetActive(true);
                        pInventory.weaponEquiped[0].gameObject.SetActive(false);



                        sfxManager.mainAudioSource.clip = sfxManager.cockingClip1;
                        sfxManager.mainAudioSource.Play();

                        pInventory.hasSecWeap = true;

                        pInventory.activeWeapIs = 1;
            

                    }
                
    }
}
