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
        if (!isDead)
        {
            if (ZombieIsInRange == false)
            {
                anim.SetBool("ZombieIsInRange", false);
                nma.SetDestination(target.position);
            }
            else
            {
                anim.SetBool("ZombieIsInRange", true);
            }
        }

        if(Health <= 0)
        {
            anim.enabled = false;
            isDead = true;
        }
    }

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

}
