using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class oldmove : MonoBehaviour
{
    CharacterController controller;
    public static bool isGrounded;
    public float can = 100;
    public Image fill;
    public Transform checker;
    public float distance = 0.3f;
    public float speed;
    private Vector3 move;
    private Vector3 moveDirection;
    [Header("Jump")]
    private Vector3 velocity;
    public float JumpHeight = 0.2f;
    public Animator gunAnim;
    public float Gravity;
    public LayerMask mask;
    [Header("Dash")]
    public float dashForce = 10f;
    public float dashDuration = 0.5f;
    public float dashCooldown =5f;
    private bool isDashing;
    public static bool isShake = false;
    [Header("TP")]
    public static Transform tpObject;
    private bool canTP=true;
    public float tpCooldown = 5f;
    private Vector3 pullDirection;
    public GameObject Bobject;
    public float speedI;
    public float pullDistance;
    public AudioSource footStep;
    public AudioSource footStepFast;
    private bool zipladim = false;
    public AudioClip FootStepClip;
    public AudioSource FootStepJump;
    public float FootStepVolume;
    private bool yonTuslatinaBasiliyor = true;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        fill.fillAmount=can/100;
    }

    private void Update()
    {   

        fill.fillAmount=can/100;
        if(can < 0)
        {
            SceneManager.LoadScene("FirstPlaceSpace");
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        move =(transform.right * horizontal + transform.forward * vertical);
        controller.Move(move * speed * Time.deltaTime);
        moveDirection = move.normalized;
        gunAnim.SetBool("isGrounded",isGrounded);

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && yonTuslatinaBasiliyor)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                gunAnim.SetBool("run", true);
                speed = 8f;
                footStepFast.enabled = true;
                footStep.enabled = false;
            }
            yonTuslatinaBasiliyor = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        }
        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)))
        {

           gunAnim.SetBool("run",false);
           footStepFast.enabled=false;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            gunAnim.SetBool("run", false);
            speed = 5f;
            footStepFast.enabled = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -3f * Gravity);
            gunAnim.SetTrigger("Jump");
            zipladim = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Dash();
        }

        isGrounded = Physics.CheckSphere(checker.position, distance, mask);

        if (isGrounded && velocity.y < 0)
        {
            if (zipladim)
            {
                FootStepJump.Play();
                zipladim = false;
            }

            velocity.y = -2f; // Reset velocity when grounded
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                footStep.enabled = true;
                if(!Input.GetKey(KeyCode.LeftShift))
                {
                  gunAnim.SetBool("walk2",true);
                }
            }else
            {
                footStep.enabled = false;
                gunAnim.SetBool("walk2",false);
            }
        }else
        {
            footStep.enabled = false;
            footStepFast.enabled = false;
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
        isDashing = true;
        isShake = true;
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
        /*if (other.gameObject.CompareTag("trambolin"))
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
        }*/
        if (other.gameObject.CompareTag("Mermi")){
            float i = 0;
            while(i<0.75f)
            {
                Invoke("Cek",i);
                i += speedI;
            }  
        }
        if (other.gameObject.CompareTag("Enemy1"))
        {
            can -= 10;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Elekto"))
        {
            TakeDamage(0.1f);
        }
    }
    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(tpCooldown);
        canTP = true;
    }

    private void Cek()
    {
        pullDirection = Bobject.transform.position - transform.position;
        controller.Move(pullDirection.normalized / pullDistance);
    }

    public void TakeDamage(float damage)
    {
        can -= damage;
        
    }

    public IEnumerator Bekle()
    {
       yield return new WaitForSeconds(0.5f);
    }
}
