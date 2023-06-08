using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpHeight = 10;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    public bool dead = false;
    public bool levelEnd = false;

    public GameObject enemy;
    public GameObject secretPassage;
    public GameObject secretPassage2;
    public GameObject secretTiles;
    public GameObject hackUpgrade;
    public GameObject hackUpgradeEnemy;
    public GameObject jumpUpgrade;
    public GameObject jumpUpgradeEnemy;
    public GameObject hackUpgradeIcon;
    public GameObject hackUpgradeText;
    public GameObject jumpUpgradeIcon;
    public GameObject jumpUpgradeText;
    public GameObject extraLife;

    private bool isJumping;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    public float hackingTime = 2f;

    public AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip endLevelSound;
    public AudioClip jumpSound;
    public AudioClip powerUpSound;
    public AudioClip hackSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (!PlayerPrefs.HasKey("Hack"))
        {
            PlayerPrefs.SetInt("Hack", 0);
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt("Hack") == 1)
            {
                hackingTime = 1f;
                hackUpgrade.SetActive(false);
                hackUpgradeIcon.SetActive(true);
            }
        }

        if (!PlayerPrefs.HasKey("Jump"))
        {
            PlayerPrefs.SetInt("Jump", 0);
            PlayerPrefs.Save();
        }
        else
        {
            if (PlayerPrefs.GetInt("Jump") == 1)
            {
                jumpHeight = 15;
                jumpUpgrade.SetActive(false);
                jumpUpgradeIcon.SetActive(true);
            }
        }
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when moving left-right
        if(horizontalInput > 0.01f && !dead && !levelEnd) {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f && !dead && !levelEnd) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        /*if(Input.GetKeyDown(KeyCode.Space) && grounded) {
            Jump();
        }*/
        
        Jump();

        if(body.velocity.y < -0.1f) {
            anim.SetTrigger("Falling");
        }

        //Set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Hacking", Input.GetKey(KeyCode.E));
        anim.SetBool("Caught", dead);

        if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(hackSound);
        }

        if (dead || levelEnd)
        {
            body.velocity = Vector2.zero;
        }

        if (dead)
        {
            StartCoroutine(Delay(2));
        }

        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        } else {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0f)
        {
            coyoteTimeCounter = 0f;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private void Jump()
    {
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            jumpBufferCounter = 0f;
            StartCoroutine(JumpCooldown());
            anim.SetTrigger("Jump");
            audioSource.PlayOneShot(jumpSound);
            grounded = false;
        }

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0f)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") {
            grounded = true;
        }

        if(collision.gameObject.tag == "Level End") {
            levelEnd = true;
            audioSource.Stop();
            audioSource.PlayOneShot(endLevelSound);
            StartCoroutine(DelayEnd(3));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Spike") {
            audioSource.PlayOneShot(deathSound);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Passage" && hackUpgradeEnemy == null) {
            secretPassage.SetActive(false);
        }

        if(other.gameObject.tag == "Passage 2" && jumpUpgradeEnemy == null) {
            secretPassage2.SetActive(false);
            secretTiles.SetActive(true);
        }

        if(other.gameObject.tag == "Hack Upgrade") {
            hackingTime = 1f;
            audioSource.PlayOneShot(powerUpSound);
            PlayerPrefs.SetInt("Hack", 1);
            PlayerPrefs.Save();
            hackUpgrade.SetActive(false);
            hackUpgradeIcon.SetActive(true);
            hackUpgradeText.SetActive(true);
            StartCoroutine(TextDelay(hackUpgradeText, 1));
        }

        if(other.gameObject.tag == "Jump Upgrade") {
            jumpHeight = 15;
            audioSource.PlayOneShot(powerUpSound);
            PlayerPrefs.SetInt("Jump", 1);
            PlayerPrefs.Save();
            jumpUpgrade.SetActive(false);
            jumpUpgradeIcon.SetActive(true);
            jumpUpgradeText.SetActive(true);
            StartCoroutine(TextDelay(jumpUpgradeText, 1));
        }

        if(other.gameObject.tag == "Life") {
            extraLife.SetActive(false);
            audioSource.PlayOneShot(powerUpSound);
        }

        if(other.gameObject.tag == "Spike") {
            dead = true;
        }
    }

    

    private IEnumerator Delay(float delayseconds)
    {
        yield return new WaitForSeconds(delayseconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator DelayEnd(float delayseconds)
    {
        yield return new WaitForSeconds(delayseconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator TextDelay(GameObject obj, float delayseconds)
    {
        yield return new WaitForSeconds(delayseconds);
        obj.SetActive(false);
    }
}
