using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDetector : MonoBehaviour
{
    [SerializeField]
    private LayerMask shipLayer;
    [SerializeField]
    private GameObject shipObj;

    private bool isPlayerOnBoard = false;

    void Update()
    {
        ShipSteering ship = shipObj.GetComponent<ShipSteering>();

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, shipLayer))
        {
            if (hit.collider.CompareTag("Ship"))
            {
                isPlayerOnBoard = true;
                transform.SetParent(hit.collider.transform);
                ship.PlayerOnBoard();
            }
        }
        else
        {
            isPlayerOnBoard = false;
            transform.SetParent(null);
        }
    }
}
