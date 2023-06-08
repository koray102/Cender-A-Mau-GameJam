using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterController controller;
    public static bool isGrounded;

    public Transform checker;
    public float distance = 0.3f;
    public float speed;
    private Vector3 move;
    private Vector3 moveDirection;
    [Header("Jump")]
    private Vector3 velocity;
    public float JumpHeight;
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
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
       

    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        move =(transform.right * horizontal + transform.forward * vertical);
        controller.Move(move * speed * Time.deltaTime);
        moveDirection = move.normalized;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            gunAnim.SetBool("run", true);
            speed = 8f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            gunAnim.SetBool("run", false);
            speed = 5f;
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Dash();
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

    }
    public IEnumerator Cooldown()
    {
       yield return new WaitForSeconds(tpCooldown);
        canTP = true;
       
    }
   
}
