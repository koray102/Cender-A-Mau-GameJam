using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            anim.SetTrigger("Contact");
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        other.transform.SetParent(null);
    }
}
