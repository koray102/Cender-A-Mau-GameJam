using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpAndDown : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1;
    public float goUpTime;
    private float timePassed;
    void Start()
    {
        goUpTime = Random.Range(1.1f, 1.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassed < goUpTime)
            {
                timePassed += Time.deltaTime;
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                
            }else if (timePassed < 2 * goUpTime)
            {
                timePassed += Time.deltaTime;
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                
            }else
            {
                timePassed = 0;
            }
    }

    private void OnTriggerEnter(Collider other) {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other) {
        other.transform.SetParent(null);
    }
}
