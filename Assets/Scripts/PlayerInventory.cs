using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Assault Rifles")]
    public string StartingWeapon = "M4";
    public GameObject[] weaponEquiped = new GameObject[2];
    public GameObject[] Unequipped;

    public ARScript arScript;

    public AudioClip cockingClip1;
    public AudioClip cockingClip2;
    public AudioSource cockingSource;

    public int activeWeapIs = 0;
    public int hasWeapInInv = 0;

    public weaponPUTZ weaponPUTZ;

    public bool hasSecWeap = false;

    public GameObject pickupNewWeapTemp;

    public int deletedUnCase = 0;

    public GameObject test11111;

    
    public void Start()
    {
        int childCounter = 0;

        Debug.Log("testing 1");

        foreach (Transform child in transform)
        {
            if (child.tag == "Weapon Binder")
            {
                foreach (Transform childOFchild in child)
                {
                    if (childOFchild.tag == "Weapon")
                    {
                        if (childOFchild.gameObject.GetComponent<ARScript>() != null)
                        {
                            Unequipped[childCounter] = childOFchild.gameObject;
                            Unequipped[childCounter].gameObject.GetComponent<ARScript>().storedWeaponNumber = childCounter;
                            childCounter = childCounter + 1;
                        }
                    }

                }
            }


        }

        

        cockingSource.clip = cockingClip1;
        cockingSource.Play();

        for (int i = 0; i < Unequipped.Length; i++)
        {

            if (Unequipped[i] != null)
            {
                Unequipped[i].gameObject.SetActive(false);
                

                if (Unequipped[i].name == StartingWeapon)
                {
                    Unequipped[i].gameObject.SetActive(true);
                    weaponEquiped[0] = Unequipped[i].gameObject;
                    Unequipped[i] = null;
                }
            }
        }
        
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (hasSecWeap == true)
            {
                if (weaponEquiped[0].gameObject.activeSelf)
                {
                    weaponEquiped[1].gameObject.SetActive(true);
                    weaponEquiped[0].gameObject.SetActive(false);

                    cockingSource.clip = cockingClip1;
                    cockingSource.Play();

                    activeWeapIs = 1;
                }

                else if (weaponEquiped[1].gameObject.activeSelf)
                {
                    weaponEquiped[1].gameObject.SetActive(false);
                    weaponEquiped[0].gameObject.SetActive(true);

                    cockingSource.clip = cockingClip2;
                    cockingSource.Play();

                    activeWeapIs = 0;
                }
            }
            
        }

        
       
    }

    public void checkEquipped()
    {
        
        foreach (GameObject temp1 in weaponEquiped)
        {
            
                if (temp1.gameObject.name == weaponPUTZ.pickupWeap.gameObject.name)
                {
                    hasWeapInInv = hasWeapInInv + 1;
                    Debug.Log(hasWeapInInv);
                }

                else
                {
                    pickupNewWeapon();


                }
                        
        }

        hasWeapInInv = 0;
    }

    

    public void pickupNewWeapon()
    {
        //Debug.Log(weaponPUTZ.)
        Debug.Log("pickup new initialised");
                if (activeWeapIs == 1)
                {
                    Unequipped[deletedUnCase] = weaponEquiped[1].gameObject;

                    for (int i = 0; i < Unequipped.Length; i++)
                    {
                        if (Unequipped[i] != null)
                        {
                            if (Unequipped[i].gameObject.name == weaponPUTZ.pickupWeap.gameObject.name)
                            {
                                Debug.Log("pickup 1 WorksFine");

                                Unequipped[i].gameObject.SetActive(true);
                                weaponEquiped[1].gameObject.SetActive(false);

                                cockingSource.clip = cockingClip1;
                                cockingSource.Play();


                                weaponEquiped[1] = Unequipped[i];

                                Unequipped[i] = null;
                                deletedUnCase = i;
                            }
                        }
                    }

            //weaponEquiped[1] = GameObject.FindGameObjectWithTag("Player_1_" + weaponPUTZ.pickupWeap.gameObject.name);
                }

        if (activeWeapIs == 0)
        {
            Unequipped[deletedUnCase] = weaponEquiped[0].gameObject;

            for (int i = 0; i < Unequipped.Length; i++)
            {
                if (Unequipped[i] != null)
                {
                    if (Unequipped[i].gameObject.name == weaponPUTZ.pickupWeap.gameObject.name)
                    {
                        Debug.Log("pickup 0 WorksFine");

                        Unequipped[i].gameObject.SetActive(true);
                        

                        cockingSource.clip = cockingClip2;
                        cockingSource.Play();

                        //weaponEquiped[0].gameObject.SetActive(false);

                        weaponEquiped[0] = Unequipped[i];

                        Unequipped[i] = null;
                        deletedUnCase = i;
                    }
                }
            }
            
        }

        

    }



    public void pickupSecWeap()
    {
        if (weaponPUTZ.pickupWeap.gameObject.name == weaponEquiped[0].gameObject.name)
        {
            Debug.Log("Cant Pickup Weapon");
        }

            else if(weaponPUTZ.pickupWeap.gameObject.name != weaponEquiped[0].gameObject.name)
            {
            
                for (int i = 0; i < Unequipped.Length; i++)
                {
                    if (Unequipped[i] != null)
                    {
                        if (Unequipped[i].gameObject.name == weaponPUTZ.pickupWeap.gameObject.name)
                        {
                            weaponEquiped[1] = Unequipped[i].gameObject;

                            deletedUnCase = i;

                            Unequipped[i] = null;


                            weaponEquiped[1].gameObject.SetActive(true);
                            weaponEquiped[0].gameObject.SetActive(false);

                            cockingSource.clip = cockingClip1;
                            cockingSource.Play();

                            activeWeapIs = 1;

                        }
                    }
                }
            }
    }
    
}
