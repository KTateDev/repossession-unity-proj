using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioController : MonoBehaviour
{
    public AudioSource pickupSoul; 
    // Start is called before the first frame update
    void Start()
    {
        pickupSoul = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        pickupSoul.Play();
    }
}
