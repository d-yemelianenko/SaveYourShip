using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
	[SerializeField]
	private GameObject shipObj;

	public int life;
	private int maxLife = 3;
	
	
	void Start()
	{
		life = Mathf.Clamp(life, 1, maxLife);
	}

	private void OnTriggerEnter(Collider other) //Efekt niszczenia przez gracza w pliku OutlineSelection
	{
		if (other.gameObject.CompareTag("Ship"))
		{
			Destroy(this.gameObject);
			ShipDurability ship = shipObj.GetComponent<ShipDurability>();
			ship.ChangeDurability(-1);
		}
		if (other.gameObject.CompareTag("IceMountain"))
		{
			Destroy(this.gameObject);
		}
	}

    private void OnDestroy()
	{
		ParticleSystem particleSystem = transform.parent.GetComponent<ParticleSystem>();
		particleSystem.Play();
	}
}