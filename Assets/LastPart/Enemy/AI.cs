using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public GameObject canvas;
    //Stats
    public float health;
public Image fill;
    public Animator aiAnim;
    public CharacterController enemyC;
    //Check for Ground/Obstacles
    public LayerMask whatIsGround, whatIsPlayer;
    private int randomskill=1;
    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attack Player
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject DamageArea;
    public ParticleSystem JumpEffect;
    public Transform JumpEffectPoint;
    public AudioSource AreaSes;
    public ParticleSystem LaserEffect;
    public Transform LaserEffectPoint;
    public AudioSource LazerSes;
    public GameObject Lazer;
    public ParticleSystem StunEffect;
    public Transform StunPoint;
    public AudioSource TakeDamageSound;
    public AudioSource StunEffectSound;
    public AudioSource deathSound;
    
    //States
    public bool isDead;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    bool canDamage=true;
   public static bool canMove=true;
    //Special
    public Material green, red, yellow;


    private void Awake()
    {
        fill.fillAmount=health/100;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (!isDead)
        {
            //Check if Player in sightrange
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

            //Check if Player in attackrange
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
        
    }

    private void Patroling()
    {
        if (isDead) return;
        aiAnim.SetBool("walk",true);
        aiAnim.SetBool("run", false);
        if (!walkPointSet) SearchWalkPoint();

        //Calculate direction and walk to Point
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

            //Vector3 direction = walkPoint - transform.position;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
        }

        //Calculates DistanceToWalkPoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

      
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        if (isDead) return;
        aiAnim.SetBool("walk", false);
        aiAnim.SetBool("run", true);
        agent.SetDestination(player.position);

    }
    private void AttackPlayer()
    {   

        if (isDead) return;

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            if (randomskill == 1 && playerInAttackRange && playerInSightRange)
            {
                attackRange = 2.5f;
                aiAnim.SetTrigger("JumpAttack");
                agent.SetDestination(player.position);
                randomskill = Random.Range(1, 4);
                alreadyAttacked = true;
                
            }
            if (randomskill == 2 && playerInAttackRange && playerInSightRange)
            {
                agent.SetDestination(transform.position);
                attackRange = 7;
                aiAnim.SetTrigger("LaserAttack");

              
               
                randomskill = Random.Range(1, 4);
                alreadyAttacked = true;
            }
            if (randomskill == 3 && playerInAttackRange && playerInSightRange)
            {

                agent.SetDestination(transform.position);
                attackRange = 15;
                aiAnim.SetTrigger("stun");
               

                randomskill = Random.Range(1, 4);
                alreadyAttacked = true;
            }





            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }

       
    }
    private void ResetAttack()
    {
        if (isDead) return;

        alreadyAttacked = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
       fill.fillAmount=health/100;
        if (health <= 0)
        {
            isDead = true;
            
            agent.enabled = false;
            aiAnim.SetTrigger("die");
            Invoke("Destroyy", 3f);
            deathSound.Play();
            enemyC.enabled = false;
            canvas.SetActive(true);
            
        }
    }
    private void Destroyy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("sword") && canDamage)
        {
            TakeDamage(15);
            TakeDamageSound.Play();
          
            // ses ve efekt
        }
    }
   
    private void JumpAttackEnable()
    {
        DamageArea.SetActive(true);

    }
    private void JumpAttackDisable()
    {
        DamageArea.SetActive(false);
    }
    void JumpAttackEffect()
    {
        Instantiate(JumpEffect, JumpEffectPoint);
        AreaSes.Play();
    }
    void LaserAttackEffect()
    {
        Instantiate(LaserEffect, LaserEffectPoint);
        LazerSes.Play();
        Instantiate(Lazer, LaserEffectPoint);

    }
    void StunAttackBegin()
    {
        StunEffectSound.Play();
        canMove = false;
        canDamage = false;
        swordmove.speed = 0f;
    }
    void StunAttackEnd()
    {   


        canMove = true;
        canDamage = true;
        swordmove.speed = 3f;
        
    }
    void StunEffectPlay()
    {
        Instantiate(StunEffect, StunPoint);
    }
}
