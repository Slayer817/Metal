using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperties : MonoBehaviour
{
    [Header("Other Scripts")]
    public WeaponProperties weapProperties;

    [Header("Gun Camera Options")]
    [Tooltip("Default value for camera field of view (40 is recommended).")]
    public float defaultFov = 40.0f;
    
    
    

    [Header("UI Components")]
    public Text currentAmmoText;
    public Text totalAmmoText;

    

    

    
    

    

    private void Start()
    {

        //gunCamera = GameObject.FindGameObjectWithTag("Player Camera").GetComponent<Camera>();
        currentAmmoText = GameObject.FindGameObjectWithTag("Current Ammo Text").GetComponent<Text>();
        totalAmmoText = GameObject.FindGameObjectWithTag("Total Ammo Text").GetComponent<Text>();

        
        //Set total ammo text from total ammo int
        totalAmmoText.text =  weapProperties.ammo.ToString();
        

    }



    

}
