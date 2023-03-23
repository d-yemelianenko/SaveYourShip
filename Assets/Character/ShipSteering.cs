using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSteering : MonoBehaviour
{
    [Header("Ship Steering")]
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private float speedUp = 0.05f;

    private float second = 0;
    private int playTimeInSeconds = 0;
    [SerializeField]
    private int SpeedUpTreshhold = 60;


    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        second += Time.deltaTime;
        if(second >= 1.0f)
        {
            second -= 1.0f;
            playTimeInSeconds += 1;

            if (playTimeInSeconds > 0 && playTimeInSeconds % SpeedUpTreshhold == 0)
                speed += speedUp;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public int getScore() //Zwraca wartoœæ czasu w sekundach jako wynik
    {
        return playTimeInSeconds;
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
