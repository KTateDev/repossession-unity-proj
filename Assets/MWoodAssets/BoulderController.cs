using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderController : MonoBehaviour
{
    public GameObject[] BoulderSpawn;
    public GameObject Boulder;
    KnightController pc;
    Rigidbody boulderRB;
    
    // Start is called before the first frame update
    void Start()
    {
        boulderRB = GetComponent<Rigidbody>();
        pc = GameObject.Find("Player").GetComponent<KnightController>();
        InvokeRepeating("spawnBoulder", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void spawnBoulder()
    {
        int i = 0;
        i = Random.Range(0, BoulderSpawn.Length - 1);
        GameObject boulder = Instantiate(Boulder, BoulderSpawn[i].transform.position, transform.rotation);
        Destroy(boulder, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        float y = boulderRB.velocity.y;
        if (other.transform.tag == "Player")
        {
            if (y == 0)
            {
                print("No Damage");
            }
            else
            {
                pc.hitMe(20, transform.position);
                print("Damage taken");
            }
        }
    }
}
