using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDetector : MonoBehaviour
{

    //public float bSpeed = 5;

    Vector3 prePos;

    void Start()
    {
        prePos = transform.position;    
    }
    
    void Update()
    {
        prePos = transform.position;

        //Debug.Log(prePos);

        //transform.Translate(0, 0, bSpeed * Time.deltaTime);

        RaycastHit[] hits = Physics.RaycastAll(new Ray(prePos, (transform.position - prePos).normalized), (transform.position - prePos).magnitude);

        for(int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].collider.gameObject.name);
        }

        Debug.DrawLine(transform.position, prePos);

    }
}
