using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTowardCamera : MonoBehaviour
{
    public GameObject target;
    public Camera cam;
    Vector3 localPos;


    // Use this for initialization
    void Start()
    {
        localPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        transform.localPosition = localPos;
    }
}