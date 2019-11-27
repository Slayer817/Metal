using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{

    NavMeshAgent nma;
    Animator anim;

    //public SphereCollider ztpCollider;
    //public BoxCollider zAttackTrigger;

    [Header("Zombie Settings")]
    public float Health = 100;

    private Collider Head;


    public Transform target;

    public bool ZombieIsInRange = false;
    public bool isDead = false;

    

    // Start is called before the first frame update
    void Start()
    {
        //Head = GameObject.FindGameObjectWithTag("Head Hitbox").GetComponent<CapsuleCollider>();
        nma = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        //ztpCollider = GameObject.FindGameObjectWithTag("ZombieToPlayerCollider").GetComponent<SphereCollider>();
        //zAttackTrigger = GetComponent<BoxCollider>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Health <= 0)
        {
            anim.speed = 0;
            Die();
            
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Detects tag");
            ZombieIsInRange = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            
            ZombieIsInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ZombieIsInRange = false;
    }
    */

    void Die()
    {
        Destroy(gameObject, 10f);
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
    }

    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        //GetComponent<Rigidbody>().isKinematic = !state;
    }

    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        //GetComponent<Collider>().enabled = !state;
    }

}
