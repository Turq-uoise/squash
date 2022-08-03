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
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void Start()
    {
        mixer.SetFloat(mixerMusic, Mathf.Log10(0.4f) * 20);
        mixer.SetFloat(mixerSFX, Mathf.Log10(0.4f) * 20);
    }

    private void SetMusicVolume(float value)
    {
        mixer.SetFloat(mixerMusic, Mathf.Log10(value) * 20);
    }

    private void SetSFXVolume(float value)
    {
        mixer.SetFloat(mixerSFX, Mathf.Log10(value) * 20);
    }
}