using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{
    private Animator anim;
    public bool caught;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        caught = FindObjectOfType<PlayerMovement>().dead;

        if (caught)
        {
            anim.enabled = false;
        }
    }

    void OnCollisionEnter2D (Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerMovement>().dead = true;
            FindObjectOfType<PlayerMovement>().audioSource.PlayOneShot(FindObjectOfType<PlayerMovement>().deathSound);
        }
    }
}
