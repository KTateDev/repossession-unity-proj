using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{

    float speed = 10;
    float rotSpeed = 150;
    float gravity = 8;
    float rot = 0;
    Vector3 moveDir = Vector3.zero;
    CharacterController controller;
    public Animator anim;
    public float robotHealth = 400;
    public float damage = 100;
    public bool isAttacking;
    public Collider swordCollider;
    int life = 100;
    public GameObject[] SpawnPoint;
    public GameObject Enemy;
    Text endGameText;
    AudioSource voice;
    public AudioClip endGameAudio;
    //public AudioClip soulGet;
    public AudioClip damageAudio;
    public AudioSource soulGet;
    public Collider soul;



    void Start()
    {
        soul = GameObject.Find("Soul").GetComponent<Collider>();
        soulGet = GetComponent<AudioSource>();
        voice = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        swordCollider = GameObject.Find("SwordCollider").GetComponent<Collider>();
        swordCollider.enabled = false;
        Enemy = GameObject.Find("Enemy");
        endGameText = GameObject.Find("EndGameText").GetComponent<Text>();
       
        InvokeRepeating("spawn", Random.Range(1, 2), Random.Range(2, 5));
   

    }


    void Update()
    {
        Movement();
        GetInput();
        // dealDamage();
        //dealDamages();
        attackWhileRunning();
        isKnightAttacking();




    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }

            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);


            }
            if (Input.GetKey(KeyCode.S))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, -1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }

            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);


            }

        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);


    }

    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButton(0))
            {
                if (anim.GetBool("running") == true)
                {
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                }
                if (anim.GetBool("running") == false)
                {
                    Attacking();
                }
            }
        }
        if (Input.GetKey("space") && Time.timeScale <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;



        }
    }
    void Attacking()
    {

        StartCoroutine(AttackRoutine());

    }
    IEnumerator AttackRoutine()
    {
        swordCollider.enabled = true;
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);    
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);
        swordCollider.enabled = false;
    }

    public void hitMe(int dmg, Vector3 sourcePos)
    {

        life -= dmg;
        voice.PlayOneShot(damageAudio);
        if (life <= 0)
        {
          
            endGameText.text = "You Lose";
           Time.timeScale = 0;

        }

    }

    public void isKnightAttacking()
    {
        if (this.anim.GetCurrentAnimatorStateInfo(1).IsName("attack"))
        {
            swordCollider.enabled = true;
            isAttacking = true;
        }
        else
        {
            swordCollider.enabled = false;
            isAttacking = false;
        }
    }

    public void attackWhileRunning()
    {

        if (Input.GetKeyDown("space"))
        {
            
            anim.SetTrigger("attack");
           // isAttacking = true;
          //  swordCollider.enabled = true;

        }
  
    }
    private void spawn()
    {
        int i = 0;
        i = Random.Range(0, SpawnPoint.Length);
        GameObject g = Instantiate(Enemy, SpawnPoint[i].transform.position, transform.rotation);
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Soul")
        {
           // voice.PlayOneShot(soulGet);
            endGameText.text = "You Win";
            anim.SetInteger("condition", 3);
            soul.enabled = false;
            //soulGet.PlayAtPoint(soulGet);
            //  endGameVoice.PlayOneShot("PickUpSoul");


        }
    }

    public float getLife()
    {
        return life;
    }


}