using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMechanic : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private GameObject waterEffects;
    [SerializeField]
    private float warmChangeValue = 3.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterStatus player = playerObj.GetComponent<CharacterStatus>();
            player.ColdChange(-warmChangeValue);
            AudioSource audioSource = waterEffects.GetComponent<AudioSource>();
            audioSource.Play(); // Odtwórz dŸwiêk przed zniszczeniem obiektu
            ParticleSystem particleSystem = waterEffects.GetComponent<ParticleSystem>();
            particleSystem.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterStatus player = playerObj.GetComponent<CharacterStatus>();
            player.ColdChange(warmChangeValue);
        }
    }

}
