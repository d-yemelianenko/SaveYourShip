using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
	[SerializeField]
	private GameObject shipObj;

	[SerializeField]
	private ParticleSystem cubeDestroyParticles;
	[SerializeField]
	private AudioSource cubeDestroyAudio;

	private void OnTriggerEnter(Collider other) //Efekt niszczenia przez gracza w pliku OutlineSelection!!!
	{
		if (other.gameObject.CompareTag("Ship"))
		{
			BeforeDestroy();
			Destroy(this.gameObject);
			ShipDurability ship = shipObj.GetComponent<ShipDurability>();
			ship.ChangeDurability(-1);
		}
		if (other.gameObject.CompareTag("IceMountain"))
		{
			Destroy(this.gameObject);
		}
	}

	public void BeforeDestroy()
	{
		ParticleSystem particleSystem = Instantiate(cubeDestroyParticles, transform.position, Quaternion.identity);
		particleSystem.Play();

		AudioSource audioSource = Instantiate(cubeDestroyAudio, transform.position, Quaternion.identity);
		audioSource.Play();
	}
}