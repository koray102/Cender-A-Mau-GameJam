using UnityEngine;

public class Enemy1Alert : MonoBehaviour
{
    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerMovement>().dead = true;
            FindObjectOfType<PlayerMovement>().audioSource.PlayOneShot(FindObjectOfType<PlayerMovement>().deathSound);
            sprite.color = new Color (1, 0, 0, 1);
        }
    }
}
