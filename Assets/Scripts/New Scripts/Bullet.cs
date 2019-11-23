﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Other Scripts")]
    public BulletProperties bProperties;
    public PlayerInventoryManager pInventory;
    public WeaponProperties wProperties;

    public GameObject collision;

    public float bulletSpeed;

    Vector3 prePos;

    void Start()
    {
        bulletSpeed = wProperties.bulletSpeed;

        //Debug.Log(ws.bulletForce);
        prePos = transform.position;

    }

    void FixedUpdate()
    {
        prePos = transform.position;

        //Debug.Log(prePos);

        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);

        RaycastHit[] hits = Physics.RaycastAll(new Ray(prePos, (transform.position - prePos).normalized), (transform.position - prePos).magnitude);



        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].collider.gameObject.name);
            //collision = hits[i].collider.gameObject;



            /*
            //If bullet collides with "Blood" tag
            if (collision.transform.tag == "Blood")
            {
                //Instantiate random impact prefab from array
                Instantiate(bs.bloodImpactPrefabs[Random.Range
                    (0, bs.bloodImpactPrefabs.Length)], transform.position,
                    Quaternion.LookRotation(hits[0].normal));
                //Destroy bullet object
                Destroy(gameObject);
            }
            */
            Debug.DrawLine(transform.position, prePos);

        }
    }

    public void Update()
    {
        if (pInventory.activeWeapIs == 0)
        {
            wProperties = pInventory.weaponEquiped[0].gameObject.GetComponent<WeaponProperties>();
        }

        else if (pInventory.activeWeapIs == 1)
        {
            wProperties = pInventory.weaponEquiped[1].gameObject.GetComponent<WeaponProperties>();
        }
    }

}
