using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class swordmove : MonoBehaviour
{
    CharacterController controller;
    public static bool isGrounded;
    public Animator swordanim;
    public Transform effectpoint;
    
    public ParticleSystem swordeffect;
    
    public AudioSource swordSound;
    public AudioSource dashSound;
    public BoxCollider sword;
    public Transform checker;
    public float distance = 0.3f;
    public static float speed=3f;
    private Vector3 move;
    public Image fill;
    private Vector3 moveDirection;
    [Header("Jump")]
    private Vector3 velocity;
    public float JumpHeight;
    public float health;
    public float Gravity;
    public LayerMask mask;
    [Header("Dash")]
    public float dashForce = 10f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 5f;
    private bool isDashing;
    public static bool isShake = false;
    [Header("TP")]
    public static Transform tpObject;
    private bool canTP = true;
    public float tpCooldown = 5f;
    public Animator camAnim;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
      fill.fillAmount=health/100;

    }

    private void Update()
    {   
          fill.fillAmount=health/100;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (!AI.canMove)
        {
            swordanim.enabled = false;
        }
        if (AI.canMove)
        {
            swordanim.enabled = true;
           // swordanim.SetBool("walk", true);
        }

        move = (transform.right * horizontal + transform.forward * vertical);
        controller.Move(move * speed * Time.deltaTime);
        moveDirection = move.normalized;
        if (health <= 0)
        {
            SceneManager.LoadScene("LastPart 1");
        }
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            swordanim.SetBool("walk", true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            swordanim.SetBool("walk", false);
        }
        if (Input.GetKey(KeyCode.LeftShift) && AI.canMove)
        {
            swordanim.SetBool("walk", true);
            speed = 5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && AI.canMove)
        {
            swordanim.SetBool("walk", false);
            speed = 3f;
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Dash();
        }
        if (Input.GetMouseButtonDown(0) && !swordanim.GetCurrentAnimatorStateInfo(0).IsName("upDownAttack") && !swordanim.GetCurrentAnimatorStateInfo(0).IsName("horizontal attack"))
        {
            swordanim.SetTrigger("attack1");
        }
        else if(Input.GetMouseButtonDown(0) && swordanim.GetCurrentAnimatorStateInfo(0).IsName("upDownAttack") && !swordanim.GetCurrentAnimatorStateInfo(0).IsName("horizontal attack"))
        {
            swordanim.SetTrigger("attack2");
        }
        else if (Input.GetMouseButtonDown(0) && swordanim.GetCurrentAnimatorStateInfo(0).IsName("horizontal attack"))
        {
            swordanim.SetTrigger("attack3");
        }


        isGrounded = Physics.CheckSphere(checker.position, distance, mask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset velocity when grounded

        }

        velocity.y += Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Dash()
    {
        if (!isDashing)
        {
            StartCoroutine(DashCoroutine());


        }
    }

    private IEnumerator DashCoroutine()
    {
        
        dashSound.Play();
        isDashing = true;
        //isShake = true;
        Vector3 dashDirection = moveDirection.normalized;
        float dashTimer = 0f;
       

        while (dashTimer < dashDuration)
        {
            dashTimer += Time.deltaTime;
            controller.Move(dashDirection * dashForce * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("trambolin"))
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -12.0f * Gravity);

        }
        if (other.gameObject.CompareTag("teleport") && canTP)
        {
            canTP = false;
            controller.enabled = false;
            transform.position = tpObject.position;
            controller.enabled = true;
            StartCoroutine(Cooldown());
        }
        if (other.gameObject.CompareTag("DamageArea"))
        {
            health -= 50;
            isShake = true;
            camAnim.SetTrigger("DamageTake");
        }
        if (other.gameObject.CompareTag("lazer"))
        {
            health -= 25;
            camAnim.SetTrigger("DamageTake");
        }
    }
    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(tpCooldown);
        canTP = true;

    }
    void Die()
    {
        Debug.Log("�ld�n");
    }
    void ColliderOpen()
    {
        sword.enabled = true;
    }
    void ColliderClose()
    {
        sword.enabled = false;
    }
    void PlaySound()
    {
        swordSound.Play();
    }
    void PlayEffect()
    {
        Instantiate(swordeffect, effectpoint.position, effectpoint.rotation);
    }
}
