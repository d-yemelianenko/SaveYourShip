using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Zapisz : MonoBehaviour
{
   
    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;
    public AudioClip[] letterSounds;

    private AudioSource audioSource;
    [HideInInspector]
    public Slider slider;
    private float _multiplier;
   
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayLetterSound(0); // Przyk³ad: Odtwarzanie dŸwiêku dla litery A
        }
        
        // Dodaj instrukcje warunkowe dla innych liter klawiatury, tak jak w przypadku litery A.
    }

    private void PlayLetterSound(int index)
    {
        if (index >= 0 && index < letterSounds.Length)
        {
            audioSource.clip = letterSounds[index];
            audioSource.Play();
            // Odczytaj aktualn¹ wartoœæ g³oœnoœci z miksera dŸwiêku
           float _volumeValue ;// na poc¹tku pobieramy zapisane parametry dzwiêku
           
           // 
            if (mixer.GetFloat(volumeParameter, out _volumeValue))
            {
                // Przelicz wartoœæ g³oœnoœci dla audioSource na podstawie wartoœci z miksera
                float soundVolume = Mathf.Pow(10f, _volumeValue / _multiplier);
                audioSource.volume = soundVolume;
            }
        }
    }
    
}


