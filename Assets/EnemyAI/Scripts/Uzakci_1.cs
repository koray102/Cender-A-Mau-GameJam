using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Uzakci_1 : MonoBehaviour
{   
    //public bool farkEdilme = false;
    
    public GameObject ucan_bomba;
    public GameObject kalkan;
    public Transform player;
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    public Transform spawn5;
    public Transform spawn6;
    public string mermitagi;
    public Image fill;
    public GameObject SonDialog;
    
    //Stats
    public float health1 = 100;
    
    
    public int dalga = 1;

    //Check for Ground/Obstacles
    //public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    //public GameObject body;
    //public Vector3 walkPoint;
    //public bool walkPointSet;
    //public float walkPointRange;

    //Attack Player
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public bool isDead;
    private bool kalkanKapali = true;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
  
    //public Material green, red, yellow;
    public GameObject projectile;
    public int gerekenYapildi = 0;
    public List<Vector3> spawnNoktalari= new List<Vector3>();
    public GameObject elektro;
    private int konum;
    private bool spikeTanitim = false;
    public AudioSource takeHit;
    public AudioClip takeHitClip;
    public float takeHitVolume;
    public GameObject duman;
   
    public GameObject canvas2;
    public GameObject dalga1;
    public GameObject dalga2;
    public GameObject dalga3;
    public GameObject dalga4;
    public GameObject dalga5;

    private void Awake()
    {
        spawnNoktalari.Add(spawn1.position);
        spawnNoktalari.Add(spawn2.position);
        spawnNoktalari.Add(spawn3.position);
        spawnNoktalari.Add(spawn4.position);
        spawnNoktalari.Add(spawn5.position);
        spawnNoktalari.Add(spawn6.position);
    }
    private void Start()
    {
        fill.fillAmount=health1/100;
    
    }
    private void Update()
    {   
        fill.fillAmount=health1/100;
        if (!isDead && kalkanKapali && Dialog.savas)
        {   
            DalgaKontrol();                
            AttackPlayer();
        } else if((dalga == 2 || dalga == 4 || dalga == 3 || dalga == 5) && Dialog2.spikeTanitimBitti)
        {
            DalgaKontrol();       
        }
        if (dalga == 2 && !spikeTanitim)
        {
            canvas2.SetActive(true);
        }       
    }

   
    private void AttackPlayer()
    {
        if (isDead) return;

        transform.LookAt(player);
        
        if (!alreadyAttacked){

            int randomInt = Random.Range(0, 20);
            
            //Attack
            GameObject mermi = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody rb = mermi.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 15f, ForceMode.Impulse);
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);

            Destroy(mermi, 3f);

            if (randomInt % 5 != 0)
            {
                alreadyAttacked = true;
                Invoke("ResetAttack", timeBetweenAttacks);
            } else
            //Tutukluk
            {
                alreadyAttacked = true;
                Invoke("ResetAttack", timeBetweenAttacks * 2);
            }
        }

        //GetComponent<MeshRenderer>().material = red;
    }
    private void ResetAttack()
    {
        if (isDead) return;

        alreadyAttacked = false;
    }
    public void TakeDamage(int damage)
    {
       if (dalga == 1)
       {
        health1 -= damage;
        takeHit.PlayOneShot(takeHitClip, takeHitVolume);
        
        if(health1< 75)
        {
            dalga1.SetActive(true);
        }
        if( health1 < 0)
        {
            dalga += 1;
        }

       } else if (dalga == 3)
       {
        health1 -= damage;
        takeHit.PlayOneShot(takeHitClip, takeHitVolume);

        if(health1< 75)
        {
            dalga3.SetActive(true);
            dalga2.SetActive(false);
        }
        if( health1 < 0)
        {
            dalga += 1;
        }

       } else if (dalga == 5)
       {
        health1 -= damage;
        takeHit.PlayOneShot(takeHitClip, takeHitVolume);
        if(health1< 75)
        {
            dalga5.SetActive(true);
            dalga4.SetActive(false);
        }
        if( health1 < 0)
        {   
            isDead = true;
            duman.SetActive(true);
            SonDialog.SetActive(true);
            dalga5.SetActive(false);
            elektro.SetActive(false);
        }
       }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    private void OnTriggerEnter(Collider other) {

        if(other.tag == mermitagi)
        {   
            if (dalga == 1)
                TakeDamage(Random.Range(20,30));
            if (dalga == 3)
                TakeDamage(Random.Range(10,20));
            if (dalga == 5)
                TakeDamage(Random.Range(5,10));
        }
    }
    private void KorumaModuAc()
    {
        kalkan.SetActive(true);
        kalkanKapali = false;
        elektro.SetActive(true);
    }
    /*private void KorumaModuKapa()
    {
        
    }*/
    
    public void DalgaKontrol()
    {
        if(dalga == 1 || dalga ==3||dalga ==5 )
        {   
            
                
                
            if(dalga == 3)
            
                timeBetweenAttacks = 2f;
            if(dalga == 5)
                timeBetweenAttacks = 1.5f;
            elektro.SetActive(true);
            gerekenYapildi = 0;
            kalkan.SetActive(false);
            kalkanKapali = true;
        }else if (dalga == 2 || dalga == 4)
        {   
            if(dalga == 2)
                dalga2.SetActive(true);
                dalga1.SetActive(false);
            if(dalga == 4)
                dalga4.SetActive(true);
                dalga3.SetActive(false);
            elektro.SetActive(false);
            kalkan.SetActive(true);
            kalkanKapali = false;
            
            if (gerekenYapildi== 0)
            {   
                gerekenYapildi = 1;
                for(int i = 0;i< 2*dalga;i++ )
                {
                konum = Random.Range(0,5);
                Instantiate(ucan_bomba,spawnNoktalari[konum],Quaternion.identity);
                }
            }
            if(CountObjectWithTags("Enemy1") == 0)
            {   
                Debug.Log(CountObjectWithTags("Enemy1"));
                dalga += 1;
            }
        }
        
    }
    private int CountObjectWithTags(string tag) {
        {
            GameObject[] gameObjectTags = GameObject.FindGameObjectsWithTag(tag);
            return gameObjectTags.Length;
        }
    }
    
}