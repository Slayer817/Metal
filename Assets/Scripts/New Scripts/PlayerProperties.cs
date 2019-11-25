using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperties : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerInventoryManager pInventory;
    public WeaponProperties wProperties;

    [Header("Gun Camera Options")]
    [Tooltip("Default value for camera field of view (40 is recommended).")]
    public float defaultFov = 40.0f;
    
    
    

    [Header("UI Components")]
    public Text currentAmmoText;
    public Text totalAmmoText;

    private bool hasFoundComponents = false;

    

    
    

    

    private void Start()
    {
       
            //gunCamera = GameObject.FindGameObjectWithTag("Player Camera").GetComponent<Camera>();
            currentAmmoText = GameObject.FindGameObjectWithTag("Current Ammo Text").GetComponent<Text>();
            totalAmmoText = GameObject.FindGameObjectWithTag("Total Ammo Text").GetComponent<Text>();

        if (!hasFoundComponents)
        {
            pInventory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<PlayerInventoryManager>();
        }
    }

    private void Update()
    {
        if (pInventory.activeWeapIs == 0)
        {
            wProperties = pInventory.weaponEquiped[0].gameObject.GetComponent<WeaponProperties>();
            Start();
            totalAmmoText.text = wProperties.ammo.ToString();
        }

        else if (pInventory.activeWeapIs == 1)
        {
            wProperties = pInventory.weaponEquiped[1].gameObject.GetComponent<WeaponProperties>();
            Start();
            totalAmmoText.text = wProperties.ammo.ToString();
        }
    }





}
