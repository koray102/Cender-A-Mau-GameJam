using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public camera cam;
    [Header("GunSettins")]
    public int range;
    //public ParticleSystem ates;
   // public Transform dusmann;
    public ParticleSystem fireEffect;
    public Transform effectpoint;
    public float bullet=10;
    public float ShootCooldown=.4f;
    bool canShoot=true;
    [Header("Animator")]
    public Animator GunAnim;
    public Uzakci_1 dusman;
    public Spike_ball dusman1;
    public AudioSource gunShot;
    public AudioClip gunShotClip;
    public float gunShotVolume;
    public AudioSource takeHitSpike;
    public AudioClip takeHitClipSpike;
    public float takeHitVolumeSpike;
    
   // public Animation ShootAnim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bullet >= 15)
        {
            bullet = 15;
        }
        if (Input.GetMouseButtonDown(0) && bullet>=1 && canShoot)
        {
            Shoot();
            gunShot.PlayOneShot(gunShotClip, gunShotVolume);
            canShoot = false;
            bullet--;
            StartCoroutine(Cooldown());
        }
        bullet += 0.01f;
        
    }
    private void Shoot()
    {
        GunAnim.SetTrigger("shoot");

        RaycastHit hit;
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                dusman.TakeDamage(Random.Range(20,30));
                //ates.Play();
            }  
            if (hit.transform.gameObject.CompareTag("Enemy1"))
            {
                takeHitSpike.PlayOneShot(takeHitClipSpike, takeHitVolumeSpike);
                Destroy(hit.transform.gameObject,0f);
            }
        }
       }
    
    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(ShootCooldown);
        canShoot = true;

    }
    public void PlayEffect()
    {
       Instantiate(fireEffect,effectpoint);
       

    }
}
