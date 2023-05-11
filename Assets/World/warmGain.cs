using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warmGain : MonoBehaviour
{
	[SerializeField]
	private GameObject playerObj;

	private void OnTriggerEnter(Collider other) //Efekt niszczenia przez gracza w pliku OutlineSelection
	{
		if (other.gameObject.CompareTag("Player"))
		{
			CharacterStatus player = playerObj.GetComponent<CharacterStatus>();
			player.ColdChange(1.5f);
		}
	}

    private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			CharacterStatus player = playerObj.GetComponent<CharacterStatus>();
			player.ColdChange(-1.5f);
		}
	}
}
