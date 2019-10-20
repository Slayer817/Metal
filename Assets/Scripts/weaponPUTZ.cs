using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponPUTZ : MonoBehaviour
{
    public PlayerInventory pInventory;
    public string weaponName;
    public Text pickupText;
    public GameObject tempGO;
    public GameObject tempInv;

    KeyCode pickup = KeyCode.E;

    private bool isOnTrigger = false;

    

    private void OnTriggerEnter(Collider other)
    {
        isOnTrigger = true;

        if (isOnTrigger == true)
        {

            if (other.gameObject.tag == "Pickable")
            {
                pickupText.text = "Pick up " + other.gameObject.name;
                tempGO = other.gameObject;
                
                
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
                    
                    pInventory.pickupSecWeap();
                    pInventory.hasSecWeap = true;
                }

                else
                {
                    
                    pInventory.checkEquipped();
                }
                
                }
            
        }
    }





}
