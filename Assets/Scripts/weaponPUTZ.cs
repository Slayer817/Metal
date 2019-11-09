﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponPUTZ : MonoBehaviour
{
    public PlayerInventory pInventory;
    public string weaponName;
    public Text pickupText;
    public GameObject pickupWeap;
    public int puWeapStoredNumber;

    KeyCode pickup = KeyCode.E;

    private bool isOnTrigger = false;

    

    private void OnTriggerEnter(Collider other)
    {
        isOnTrigger = true;

        if (isOnTrigger == true)
        {

            if (other.gameObject.tag == "Pickable") //Check if weapon on maps have the Pickable Tag
            {
                pickupText.text = "Pick up " + other.gameObject.name;
                pickupWeap = other.gameObject;

                for (int i = 0; i < pInventory.Unequipped.Length; i++)
                {
                    if(pInventory.Unequipped[i] != null)
                    {
                        if(pickupWeap.gameObject.name == pInventory.Unequipped[i].gameObject.name)
                        {
                            Debug.Log("Dhomer");
                        }
                    }
                }
                
                if (pickupWeap.gameObject.name == pInventory.weaponEquiped[0].gameObject.name)
                {
                    Debug.Log("Cant Pick Up 1");
                }

                else if(pInventory.weaponEquiped[1].gameObject != null)
                {
                    if (pickupWeap.gameObject.name == pInventory.weaponEquiped[1].gameObject.name)
                    {
                        Debug.Log("Cant Pick Up 2");
                    }
                }

                
            }
            

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        pickupText.text = "";
        isOnTrigger = false;
        
    }

    private void Update()
    {        
        if(isOnTrigger == true)
        {
            if(Input.GetKeyDown(pickup))
            {
                    
                

                //pInventory.ChangeWeapons();

                if(pInventory.hasSecWeap == false)
                {
                    if (pickupWeap.gameObject.name != pInventory.weaponEquiped[0].gameObject.name)
                    {                       
                        pInventory.pickupSecWeap();
                        pInventory.hasSecWeap = true;
                    }
                }

                else
                {
                    
                    pInventory.checkEquipped();
                }
                
            }
            
        }
    }





}
