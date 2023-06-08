using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject asteroidPrefab1;
    public GameObject asteroidPrefab2;
    public GameObject asteroidPrefab3;
    public float startDelay1 = 0;
    public float startDelay2 = 3;
    public float startDelay3 = 5;
    public float repeateInterval;

    void Start()
    {
        InvokeRepeating("InstatiateAsteroid1", startDelay1, repeateInterval);
        InvokeRepeating("InstatiateAsteroid2", startDelay2, repeateInterval);
        InvokeRepeating("InstatiateAsteroid3", startDelay3, repeateInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstatiateAsteroid1()
    {
        Instantiate(asteroidPrefab1, asteroidPrefab1.transform.position, asteroidPrefab1.transform.rotation);
    }

    void InstatiateAsteroid2()
    {
        Instantiate(asteroidPrefab2, asteroidPrefab2.transform.position, asteroidPrefab2.transform.rotation);
    }

    void InstatiateAsteroid3()
    {
        Instantiate(asteroidPrefab3, asteroidPrefab3.transform.position, asteroidPrefab3.transform.rotation);
    }
}
