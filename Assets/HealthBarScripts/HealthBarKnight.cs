using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarKnight : MonoBehaviour
{
    public Image healthBar;
    public float fill;
    public KnightController target;

    // Start is called before the first frame update
    void Start()
    {
        fill = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (target.getLife()) / 100;
    }
}