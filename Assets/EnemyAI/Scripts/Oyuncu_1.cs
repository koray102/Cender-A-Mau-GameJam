using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oyuncu_1 : MonoBehaviour
{   
    public CharacterController cc;
    public Transform characterTransform;
    public float timeToPull;
    private float timePassed;
    public string objectATag = "Mermi"; // "A" tagli objenin etiketi
    //public string objectBTag = "ObjectB"; // "B" tagli objenin etiketi

    //private GameObject objectA; // "A" tagli objenin referansı
    public GameObject objectB; // "B" tagli objenin referansı

    private Vector3 pullDirection;

    //public float pullSpeed = 5f; // Çekme hızı

    private void Start()
    {
      
        
    }

    private void Update()
    {
        


    }


    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag(objectATag) )
        {
            
            //Vector3 pullDirection = objectB.transform.position - transform.position;
            //cc.Move(pullDirection);
            
            float i = 0;
            while(i<0.75f)
            {
                Invoke("Cek",i);
                i += 0.01f;
            }  
        }
    }
    private void Cek(){
        pullDirection = objectB.transform.position - transform.position;
        cc.Move(pullDirection/2000);
    }
    
    
}
