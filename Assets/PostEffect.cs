using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostEffect : MonoBehaviour
{
    public PostProcessProfile profile; // Reference to the PostProcessProfile
    private Vignette vignette; // Reference to the Vignette effect
    private void Start()
    {
        // Get the Vignette effect from the PostProcessProfile
        vignette = profile.GetSetting<Vignette>();
    }
    // Update is called once per frame
  void vignetteOn()
    {
        vignette.intensity.value = .5f;
    }
    void vignetteOf()
    {
        vignette.intensity.value = 0f;
    }
    void vignette40()
    {
        vignette.intensity.value = 0.4f;
    }
    void vignette30()
    {
        vignette.intensity.value = 0.3f;
    }
    void vignette20()
    {
        vignette.intensity.value = 0.2f;
    }
}
