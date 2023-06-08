using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSonat : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private float timePassed;
    public float timePassedLimit;
    public Transform targetPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            move();
        }
    }

    void move()
    {
        while (transform.position != targetPosition.position)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime);
        }
    }
}
