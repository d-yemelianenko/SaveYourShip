using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeInto : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(volumeParameter, volumeParameter == "FireVol" ? 0f : 0f);
        mixer.SetFloat(volumeParameter, volumeValue);
    }
}



