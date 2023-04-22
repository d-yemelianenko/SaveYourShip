using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
	[SerializeField]
	private GameObject shipObj;

	private Transform playerCamera;
	private bool isLookingAtCube = false;
	public KeyCode interactionKey = KeyCode.Mouse0;

	public int life;
	private int maxLife = 3;
	
	
	void Start()
	{
		life = Mathf.Clamp(life, 1, maxLife);
		playerCamera = Camera.main.transform;
	}

	void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 7f))
		{
			if (hit.transform == transform)
			{
				isLookingAtCube = true;
			}
			else
			{
				isLookingAtCube = false;
			}
		}
		else
		{
			isLookingAtCube = false;
		}

		if (isLookingAtCube && Input.GetKeyDown(interactionKey))
        {
			Destroy(this.gameObject);
		}
	}


	private void OnTriggerEnter(Collider other) //TODO efekt zniszczenia, inny sposób zniszczenia.
	{
		if (other.gameObject.CompareTag("Ship"))
		{
			Destroy(this.gameObject);
			ShipDurability ship = shipObj.GetComponent<ShipDurability>();
			ship.ChangeDurability(-1);
		}
	}
}