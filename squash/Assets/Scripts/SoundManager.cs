using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    const string mixerMusic = "MusicVolume";
    const string mixerSFX = "SFXVolume";

    private void Awake()
    {

    }

    void Start()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            mixer.SetFloat(mixerMusic, Mathf.Log10(0.4f) * 20);
            PlayerPrefs.SetFloat("musicVolume", 0.4f);
        }
        else
        {
            Load();
        }
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            mixer.SetFloat(mixerSFX, Mathf.Log10(0.4f) * 20);
            PlayerPrefs.SetFloat("sfxVolume", 0.4f);
        }
        else
        {
            Load();
        }
    }

    private void SetMusicVolume(float value)
    {
        mixer.SetFloat(mixerMusic, Mathf.Log10(value) * 20);
        Save();
    }

    private void SetSFXVolume(float value)
    {
        mixer.SetFloat(mixerSFX, Mathf.Log10(value) * 20);
        Save();
    }

    private void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }
}