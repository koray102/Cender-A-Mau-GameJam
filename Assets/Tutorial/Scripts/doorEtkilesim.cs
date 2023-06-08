using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorEtkilesim : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject toEnter;
    public GameObject startDialog;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && toEnter != null)
        {
            toEnter.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F) )
            {
                startDialog.SetActive(true);
                Destroy(toEnter);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && toEnter != null)
        {
            toEnter.SetActive(false);
        }
    }
}
