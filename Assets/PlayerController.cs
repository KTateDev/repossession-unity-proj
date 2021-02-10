using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer myRend;
    CharacterController myCC;
    int life = 100;
    void Start()
    {
        myRend = GetComponent<Renderer>();
        InvokeRepeating("changeColor",0,1);
        myCC = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 step = transform.forward * v + transform.right * h;
        transform.position += step * 5 * Time.deltaTime;
        myCC.Move(step * 5 * Time.deltaTime);
    }
    void changeColor()
    {
        myRend.material.color = Random.ColorHSV();
    }

    public void hitMe(int dmg, Vector3 sourcePos)
    {
        life -= dmg;
        if(life <= 0)
        {
            Time.timeScale = 0;
        }
        Vector3 v = transform.position - sourcePos;
        v.y = 0;
        myCC.Move(v * 3);
    }
}
