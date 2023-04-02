using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 CubePosition;

    [Header("Wymiary mapy")]
    [SerializeField]
    private float mapWidth = 60;
    [SerializeField]
    private float mapLength = 540;


    void Start()
    {
        for (var x = 0; x < mapWidth; x+=2)
        {
            for(var z = 0; z < mapLength; z+=2)
            {
                CubePosition = prefab.transform.position;
                
                Instantiate(prefab, new Vector3(CubePosition[0] + x, CubePosition[1], CubePosition[2] + z), Quaternion.identity);
            }
        }
    }

    void Update()
    {

    }
}
