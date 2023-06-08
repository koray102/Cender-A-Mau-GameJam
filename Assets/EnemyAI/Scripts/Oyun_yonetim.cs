using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Oyun_yonetim : MonoBehaviour
{  
     public GameObject canvas1;
     public GameObject dumanlar;

     public bool acildi = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && !acildi){
            canvas1.SetActive(true);
            dumanlar.SetActive(false);
            acildi = true;
        }
    }
}
