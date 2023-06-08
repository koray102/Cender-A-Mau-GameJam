using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Toggle fullscreen;
    public Slider volumeSlider;
    public Text volumeText;
    AudioSource audioSource;
    public float volume;


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void ChangeVolume()
    {
        volume = volumeSlider.value;
        audioSource.volume = volume/100;
        Save();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (!audioSource.playOnAwake)
        {
            audioSource.Play();
        }

        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 100);
            Load();
        } else {
            Load();
        }
    }

    private void Update()
    {
        volumeText.text = volumeSlider.value.ToString();

        if (fullscreen.isOn)
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
        } else {
            Screen.SetResolution(1440, 810, FullScreenMode.Windowed);
        }
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
    
}
