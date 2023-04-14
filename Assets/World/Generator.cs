using System.Collections;
using System;
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
        GameObject parentObject = new GameObject("CubeParent");

        for (var x = 0; x < mapWidth; x += 2)
        {
            for (var z = 0; z < mapLength; z += 2)
            {
                System.Random rnd = new System.Random();
                int randomInt = rnd.Next(0, 20);

                CubePosition = prefab.transform.position;
                if ((x < 25 || x > 35 || z > 45) && randomInt != 0)
                {
                    GameObject cube = Instantiate(prefab, new Vector3(CubePosition[0] + x, CubePosition[1], CubePosition[2] + z), Quaternion.identity);
                    cube.transform.parent = parentObject.transform;
                }
            }
        }
    }
}
