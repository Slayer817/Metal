using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDetector : MonoBehaviour
{

    public float bSpeed = 5;

    //public AutomaticWeaponScript ws;

    Vector3 prePos;

    void Start()
    {

        //Debug.Log(ws.bulletForce);
        prePos = transform.position;

        //GetComponent<Rigidbody>().velocity = transform.forward * Time.deltaTime * 20;

        //GetComponent<Rigidbody>().AddForce(transform.forward * 40);

    }
    
    void FixedUpdate()
    {
        prePos = transform.position;

        //Debug.Log(prePos);

        transform.Translate(Vector3.forward * Time.deltaTime * bSpeed);

        RaycastHit[] hits = Physics.RaycastAll(new Ray(prePos, (transform.position - prePos).normalized), (transform.position - prePos).magnitude);

        for(int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].collider.gameObject.name);
        }

        Debug.DrawLine(transform.position, prePos);

    }
}
