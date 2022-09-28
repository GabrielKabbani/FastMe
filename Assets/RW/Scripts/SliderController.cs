using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderController : MonoBehaviour{   
    public Slider slider;
    public float sliderValue;
    public AudioMixer audioMixer;

    void Start(){
        slider.value = PlayerPrefs.GetFloat("save", sliderValue);
    }

    public void SetVolume(float volume){
        sliderValue = volume;
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("save", volume);
    }
}
