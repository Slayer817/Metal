using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("Assault Rifles")]
    public GameObject[] weaponEquiped = new GameObject[2];
    public GameObject[] Unequipped;

    public AudioClip cockingClip1;
    public AudioClip cockingClip2;
    public AudioSource cockingSource;

    public int activeWeapIs = 0;
    public int hasWeapInInv = 0;

    public weaponPUTZ weaponPUTZ;

    public bool hasSecWeap = false;

    public GameObject pickupNewWeapTemp;

    public int deletedUnCase = 0;

    
    public void Start()
    {
        
        weaponEquiped[0] = GameObject.FindGameObjectWithTag("Player_1_M4");
        //weaponEquiped[1] = GameObject.FindGameObjectWithTag("weaponEquiped 1");

        //Assault Rifles
        //Unequipped[0] = GameObject.FindGameObjectWithTag("Player_1_M4");
        Unequipped[1] = GameObject.FindGameObjectWithTag("Player_1_AK-47");
        Unequipped[2] = GameObject.FindGameObjectWithTag("Player_1_SCAR");

        //Pistols
        Unequipped[10] = GameObject.FindGameObjectWithTag("Player_1_P99");
        Unequipped[11] = GameObject.FindGameObjectWithTag("Player_1_Beretta 92SB");
        Unequipped[12] = GameObject.FindGameObjectWithTag("Player_1_M1911");
        Unequipped[13] = GameObject.FindGameObjectWithTag("Player_1_USP .45");

        //SMGs
        Unequipped[20] = GameObject.FindGameObjectWithTag("Player_1_Vector");
        Unequipped[21] = GameObject.FindGameObjectWithTag("Player_1_Agram 2000");
        Unequipped[22] = GameObject.FindGameObjectWithTag("Player_1_Uzi");
        Unequipped[23] = GameObject.FindGameObjectWithTag("Player_1_P90");
        Unequipped[24] = GameObject.FindGameObjectWithTag("Player_1_MP5");

        //Snipers
        Unequipped[30] = GameObject.FindGameObjectWithTag("Player_1_R700");
        Unequipped[31] = GameObject.FindGameObjectWithTag("Player_1_Barrett .50Cal");
        Unequipped[32] = GameObject.FindGameObjectWithTag("Player_1_Dragunov");

        //Shotguns and others
        Unequipped[40] = GameObject.FindGameObjectWithTag("Player_1_Model 1100");
        Unequipped[41] = GameObject.FindGameObjectWithTag("Player_1_GL06");
        Unequipped[42] = GameObject.FindGameObjectWithTag("Player_1_RPG");

        cockingSource.clip = cockingClip1;
        cockingSource.Play();

        for (int i = 0; i < Unequipped.Length; i++)
        {
            if(Unequipped[i] != null)
            {
                Unequipped[i].gameObject.SetActive(false);
            }
        }

        /*foreach (GameObject temp in Unequipped)
        {
            
            temp.SetActive(false);
        }*/

        Debug.Log(weaponEquiped[0].name);

        //weaponEquiped[1].gameObject.SetActive(false);
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
            
                if (temp1.gameObject.name != weaponPUTZ.tempGO.gameObject.name)
                {
                    hasWeapInInv = hasWeapInInv + 1;
                    Debug.Log(hasWeapInInv);
                }

                if (hasWeapInInv == 2)
                {
                    pickupNewWeapon();


                }
                        
        }

        hasWeapInInv = 0;
    }

    public void pickupNewWeapon2()
    {
        if(activeWeapIs == 0)
        {

        }
    }

    public void pickupNewWeapon()
    {

                if (activeWeapIs == 1)
                {
                    Unequipped[deletedUnCase] = weaponEquiped[1].gameObject;

                    for (int i = 0; i < Unequipped.Length; i++)
                    {
                        if (Unequipped[i] != null)
                        {
                            if (Unequipped[i].gameObject.name == weaponPUTZ.tempGO.gameObject.name)
                            {
                                Debug.Log("pickupWorksFine");

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

            //weaponEquiped[1] = GameObject.FindGameObjectWithTag("Player_1_" + weaponPUTZ.tempGO.gameObject.name);
                }

        if (activeWeapIs == 0)
        {
            Unequipped[deletedUnCase] = weaponEquiped[0].gameObject;

            for (int i = 0; i < Unequipped.Length; i++)
            {
                if (Unequipped[i] != null)
                {
                    if (Unequipped[i].gameObject.name == weaponPUTZ.tempGO.gameObject.name)
                    {
                        Debug.Log("pickupWorksFine");

                        Unequipped[i].gameObject.SetActive(true);
                        weaponEquiped[0].gameObject.SetActive(false);

                        cockingSource.clip = cockingClip2;
                        cockingSource.Play();

                        weaponEquiped[0] = Unequipped[i];

                        Unequipped[i] = null;
                        deletedUnCase = i;
                    }
                }
            }

            //weaponEquiped[1] = GameObject.FindGameObjectWithTag("Player_1_" + weaponPUTZ.tempGO.gameObject.name);
        }

        /*if (activeWeapIs == 0)
        {
            foreach (GameObject temp in Unequipped)
            {

                if (temp.gameObject.name == weaponPUTZ.tempGO.gameObject.name)
                {
                    pickupNewWeapTemp = GameObject.FindGameObjectWithTag("Player_1_" + weaponEquiped[0].gameObject.name);
                    weaponEquiped[0] = temp.gameObject;

                    weaponEquiped[0].gameObject.SetActive(true);

                    pickupNewWeapTemp.gameObject.SetActive(false);


                }
            }
        }

        if (activeWeapIs == 1)
        {
            foreach (GameObject temp in Unequipped)
            {

                if (temp.gameObject.name == weaponPUTZ.tempGO.gameObject.name)
                {

                    pickupNewWeapTemp = GameObject.FindGameObjectWithTag("Player_1_" + weaponEquiped[1].gameObject.name);
                    weaponEquiped[1] = temp.gameObject;

                    weaponEquiped[1].gameObject.SetActive(true);

                    pickupNewWeapTemp.gameObject.SetActive(false);


                }
            }
        }*/

    }

    

    public void pickupSecWeap()
    {

        for (int i = 0; i < Unequipped.Length; i++)
        {
            if (Unequipped[i] != null)
            {
                if (Unequipped[i].gameObject.name == weaponPUTZ.tempGO.gameObject.name)
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

        /*foreach (GameObject temp in Unequipped)
        {

            if (temp.gameObject != null)
            {



                if (temp.gameObject.name == weaponPUTZ.tempGO.gameObject.name)
                {
                    weaponEquiped[1] = temp.gameObject;


                    weaponEquiped[1].gameObject.SetActive(true);
                    weaponEquiped[0].gameObject.SetActive(false);

                    activeWeapIs = 1;

                }
            }
        }*/
        
        //weaponEquiped[1] = GameObject.FindGameObjectWithTag("Player_1_" + weaponPUTZ.tempGO.gameObject.name);
    }
    
}
