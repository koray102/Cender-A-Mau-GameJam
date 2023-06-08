using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_ball : MonoBehaviour
{
    public Transform target; // Hedef objenin referansı
    public float speed = 5f; // Uçuş hızı
    public ParticleSystem explodeEffect;
    
    private void Start()
    {
       
        
    }

    private void Update()
    {   

        GameObject takipEdilenObj = GameObject.FindGameObjectWithTag("Player");
        
        // Takip edilen obje varsa
        if (takipEdilenObj != null && Dialog2.spikeTanitimBitti)
        {
            Vector3 yon = takipEdilenObj.transform.position - transform.position;
            yon.Normalize();
            
            transform.position += yon * speed * Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter(Collider other) {
       
        if(other.CompareTag("Player"))
        {
            Instantiate(explodeEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
