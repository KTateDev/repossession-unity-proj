using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class TheifController : MonoBehaviour
{
    Animator my_animator;
   // bool isRunning;
    Transform waypointLoc1;
    Transform waypointLoc2;
    Transform waypointLoc3;
    Transform waypointLoc4;
    Collider waypoint;
    Transform playerTransform;
    bool changeDirection;
    NavMeshAgent myAgent;
    int runDirection;
    Animator die;
    public float health = 400;
    //GameObject knight;
    Collider platformCol;
    MeshRenderer platformMesh;
    KnightController knight;
    ParticleSystem runTrail;
    ParticleSystem deathEffect;
    GameObject batEnemy;
    AudioSource thiefDefeated;
    AudioClip enemyDefeated;
    

    // Start is called before the first frame update
    void Start()
    {
        batEnemy = GameObject.Find("Enemy").GetComponent<GameObject>();
        myAgent = GetComponent<NavMeshAgent>();
        platformCol = GameObject.Find("SoulPlatform").GetComponent<Collider>();
        platformMesh = GameObject.Find("SoulPlatform").GetComponent<MeshRenderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        waypointLoc1 = GameObject.Find("Waypoint1").GetComponent<Transform>();
        waypointLoc2 = GameObject.Find("Waypoint2").GetComponent<Transform>();
        waypointLoc3 = GameObject.Find("Waypoint3").GetComponent<Transform>();
        waypointLoc4 = GameObject.Find("Waypoint4").GetComponent<Transform>();
        waypoint = GameObject.Find("Waypoint1").GetComponent<Collider>();
        my_animator = GetComponent<Animator>();
      //  isRunning = true;
        transform.LookAt(waypointLoc1.position);
        changeDirection = false;
        die = GetComponent<Animator>();
        my_animator.SetBool("isRunning", true);
        knight = GameObject.Find("Player").GetComponent<KnightController>();
        runTrail = GameObject.Find("RunningTrail").GetComponent<ParticleSystem>();
        deathEffect = GameObject.Find("DeathEffect").GetComponent<ParticleSystem>();
        platformCol.isTrigger = true;
        platformMesh.enabled = false;
        my_animator.speed += 0.1f;
        enemyDefeated = GetComponent<AudioClip>();
        thiefDefeated = GetComponent<AudioSource>();

        //***Uncomment to test for sword collision**//
        //my_animator.SetBool("isRunning", false);

       


    }

    // Update is called once per frame
    void Update()
    {
        if(my_animator.speed >= 1.5f)
        {
            my_animator.speed = 1.5f;
        }


        if (changeDirection == false)
        {
            transform.LookAt(waypointLoc1.position);
            myAgent.SetDestination(waypointLoc1.position);
        }

        
        if (health <= 0 )
        {
         
            platformCol.isTrigger = false;
            platformMesh.enabled = true;
            //***dying animation***
            my_animator.SetBool("isRunning", false);
            runTrail.Stop();
            deathEffect.Play();
            
            Destroy(GameObject.FindWithTag("Bat"));
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "SwordCollider")
        {
            knight.isKnightAttacking();
            knight.attackWhileRunning();      
            if (knight.isAttacking == true)
            {
               
                health -= 100;
                print("Damage dealt" + health);
            }
        }
      
            getLife();
        if (health <= 0){
               thiefDefeated.Play();
        }

        /*
        if (other.tag == "SwordCollider")
        {
            knight.dealDamage();
            if (knight.isAttacking == true)
            {
                health -= 100;
                print("Damage dealt" + health);
            }
        }
        */



        runDirection = Random.Range(0, 2);
        if (other.tag == "Waypoint1")
        {
            my_animator.speed += 0.1f;

            changeDirection = true;
            if (runDirection == 0)
            {
                myAgent.SetDestination(waypointLoc2.position);
            }
            else if(runDirection == 1)
            {
                myAgent.SetDestination(waypointLoc4.position);
            }
        }else if (other.tag == "Waypoint2")
        {
            my_animator.speed += 0.1f;
            changeDirection = true;
            if (runDirection == 0)
            {
                myAgent.SetDestination(waypointLoc3.position);
            }
            else if (runDirection == 1)
            {
                myAgent.SetDestination(waypointLoc1.position);
            }
        }
        else if (other.tag == "Waypoint3")
        {
            my_animator.speed += 0.1f;
            changeDirection = true;
            if (runDirection == 0)
            {
                myAgent.SetDestination(waypointLoc4.position);
            }
            else if (runDirection == 1)
            {
                myAgent.SetDestination(waypointLoc2.position);
            }
        }
        else if (other.tag == "Waypoint4")
        {
            my_animator.speed += 0.1f;
            changeDirection = true;
            if (runDirection == 0)
            {
                myAgent.SetDestination(waypointLoc1.position);
            }
            else if (runDirection == 1)
            {     
                myAgent.SetDestination(waypointLoc3.position);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {


       
    }

    public float getLife()
    {
        return health;
    }


}
