using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Image healthBar;
    public float fill;
    public TheifController target;

    // Start is called before the first frame update
    void Start()
    {
        //fill = 1f;
        fill -= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (target.getLife()) / 400;
    }
}