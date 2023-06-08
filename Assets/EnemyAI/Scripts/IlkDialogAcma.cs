using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlkDialogAcma : MonoBehaviour
{   
    public GameObject dialog;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bekle());
        Bekle();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Bekle()
    {
        yield return new WaitForSeconds(5f);
        dialog.SetActive(true);
    }
}
