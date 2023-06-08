using UnityEngine;
using UnityEngine.UI;

public class Enemy2Hitbox : MonoBehaviour
{
    public Slider slider;
    public GameObject sliderObject;
    public GameObject hackingKey;
    public float sliderStartingValue;
    public float sliderEndValue;
    public float sliderMinValue;
    public bool canBeHacked = false;

    void Start()
    {
        sliderMinValue = 0;
    }

    void Update()
    {
        sliderEndValue = FindObjectOfType<PlayerMovement>().hackingTime / 10;
        slider.maxValue = sliderEndValue;

        if (canBeHacked)
        {
            Hack();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") {
            canBeHacked = true;
            hackingKey.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") {
            canBeHacked = false;
            hackingKey.SetActive(false);
        }
    }

    public void Hack()
    {
        if (Input.GetKey(KeyCode.E))
        {
            sliderObject.SetActive(true);
            hackingKey.SetActive(false);
            sliderStartingValue = Mathf.MoveTowards(sliderStartingValue, sliderEndValue, Time.deltaTime);
        }
        else
        {
            sliderStartingValue = sliderMinValue;
            sliderObject.SetActive(false);
            hackingKey.SetActive(true);
        }

        slider.value = sliderStartingValue;

        if(sliderStartingValue == sliderEndValue) {
            Destroy(transform.parent.parent.gameObject);
            canBeHacked = false;
        }
    }
}
