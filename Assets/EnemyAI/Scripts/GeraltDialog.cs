using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TextMeshProUGUI = TMPro.TextMeshProUGUI;

public class GeraltDialog : MonoBehaviour
{   
    public static bool savas = false;
    public string textTyping;
    public TextMeshProUGUI dialogText;
    public float typingSpeed = 0.05f;
    public List<string> lines;
    private bool isTyping = true;
    private bool isFinished = false;

    
    private void Start()
    {
        dialogText.text = "";
        StartCoroutine(TypeText());
   
    }

    private void Update()
    {
        if (isTyping)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isTyping = false;
                FinishTyping();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                CloseCanvas();
            }
        }
    }

    IEnumerator TypeText()
    {
        foreach (string line in lines)
        {
            foreach (char c in line)
            {
                dialogText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            dialogText.text += "\n";
        }

        isTyping = false;
        isFinished = true;
    }

    private void FinishTyping()
    {
        if (!isFinished)
        {
            StopAllCoroutines();
            dialogText.text = string.Join("\n", lines.ToArray());
            isFinished = true;
        }
    }

    private void CloseCanvas()
    {
        // Burada canvas'i kapatmak için gerekli kodu ekleyin
        Destroy(gameObject);
        StartCoroutine(Bekle(10));
        savas = true;
    }
    private IEnumerator Bekle(int saniye)
    {
        yield return new WaitForSeconds(saniye);
        savas = true;
    }

}