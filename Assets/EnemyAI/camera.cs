using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{   
    [Header("CAM")]
    public float sensitivity = 2f;
    public Transform playerBody;
    
    private float xRotation = 0f;
    public Transform cam;
    [Header("Shake")]
    public float shakeDuration = 0.3f;
    public float shakeIntensity = 0.5f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;




    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get the mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player's body based on horizontal mouse movement
        playerBody.Rotate(Vector3.up * mouseX);
        

        // Calculate vertical rotation for the camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        if (oldmove.isShake)
        {
            Shake();
            oldmove.isShake = false;
        }
     
    }
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
     
    }
    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float randomPosX = Random.Range(-1f, 1f) * shakeIntensity;
            float randomPosY = Random.Range(-1f, 1f) * shakeIntensity;

            transform.localPosition = originalPosition + new Vector3(randomPosX, randomPosY, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
    }
}
