using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumControl : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;
    public Slider slider;
    private const float _multiplier = 20f;
    private float _volumeValue;
    public float soundVolume;
    // Start is called before the first frame update
    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        _volumeValue = Mathf.Log10(value) * _multiplier;
        mixer.SetFloat(volumeParameter, _volumeValue);
    }
    void Start()
    {
        _volumeValue = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(slider.value) * _multiplier);// na poc¹tku pobieramy zapisane parametry dzwiêku
        slider.value = Mathf.Pow(10f, _volumeValue / _multiplier);
       // Debug.Log(_volumeValue);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter,_volumeValue ); //zapisujemy parametry muzyki
    }

   
    
}
