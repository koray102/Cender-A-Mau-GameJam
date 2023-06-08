using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    public GameObject enemyObject;
    public bool facingLeft;
    public bool caught;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    void Update()
    {
        if (enemyObject.transform.localScale.x < 0){
            facingLeft = true;
        } else {
            facingLeft = false;
        }

        //anim.SetBool("Alert", caught);
        //anim.SetBool("Facing Left", facingLeft);

        caught = FindObjectOfType<PlayerMovement>().dead;

        if (caught)
        {
            anim.enabled = false;
        }
    }

    private void AnimateSprite()
    {
        spriteIndex = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
