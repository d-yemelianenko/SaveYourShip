using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
  
    public AudioSource audioSource;
    public AudioClip hoverAudio;
    public AudioClip clickAudio;



    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverAudio);
    }

    public void ClipSound()
    {
        audioSource.PlayOneShot(clickAudio);
    }

}
