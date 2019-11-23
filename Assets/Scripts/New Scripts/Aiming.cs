using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    [Header("Other Scripts")]
    public PlayerController pController;

    [Space(20)]
    [Header("Aiming Backgrounds")]
    public GameObject aimingBG;

    private bool hasFoundComponents = false;

    public void Start()
    {
        if(hasFoundComponents == false)
        {
            aimingBG = gameObject.transform.GetChild(0).gameObject;
            pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            
            hasFoundComponents = true;
        }

               

    }

    public void Update()
    {
        if (pController.isAiming == true)
        {
            aimingBG.SetActive(true);
        }

        if (pController.isAiming == false)
        {
            aimingBG.SetActive(false);
        }
    }


}
