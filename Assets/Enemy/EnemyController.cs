using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Transform playerTransform;
    NavMeshAgent myAgent;
    KnightController pc;
    public GameObject SpawnPoint;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
       // gameObject.GetComponent<Renderer>().enabled = false;
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        pc = GameObject.Find("Player").GetComponent<KnightController>();
        myAgent = GetComponent<NavMeshAgent>();
     

    }

    // Update is called once per frame
    void Update()
    {
        myAgent.SetDestination(playerTransform.position);
        //transform.LookAt(playerTransform.position);
       // transform.position += transform.forward * 5 * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {

            pc.hitMe(20, transform.position);
            print("Damage taken");
            Destroy(Enemy);
        }


    }

}
