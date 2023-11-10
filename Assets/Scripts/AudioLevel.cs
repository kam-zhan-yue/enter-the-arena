using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioLevel : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    void Start()
    {
        if (PlayerPrefs.HasKey("Volume") == false)
            PlayerPrefs.SetFloat("Volume", 100);
        slider.value = PlayerPrefs.GetFloat("Volume");
    }

    //===============PROCEDURE===============//
    public void SetVolume()
    //Purpose:          Sets the volume of the slider and changes playerprefs on slider value
    {
        float volume = slider.value;
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
