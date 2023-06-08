using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer2 : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    public float speed = 120f; // Speed of the bullet
    private void Start()
    {
         Destroy(gameObject, 1.2f);
        player = GameObject.FindGameObjectWithTag("hit").transform;
    }
    private void Update()
    {
        if (player == null)
        {
            // If the player is null (not found or destroyed), destroy the bullet
            Destroy(gameObject);
            return;
        }

        // Calculate the direction from the bullet to the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the bullet towards the player
        transform.position += direction * speed * Time.deltaTime;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

