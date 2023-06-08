using UnityEngine;

public class Mermi : MonoBehaviour
{
    public Transform target;
    public float speed = 50f;
    public float stoppingDistance = 1f;

    private int sayac= 0;
    

    private void Start()
    {
        
        
    }

    private void FixedUpdate()
    {   
        
       
    }
    private void OnCollisionEnter(Collision other) {
        if(sayac == 0)
        {
          Destroy(gameObject, 0f);  
        } else{
            sayac += 1;
        }
        
    }
}
