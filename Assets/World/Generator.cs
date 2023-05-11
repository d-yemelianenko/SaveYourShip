using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject iceCube;
    public GameObject sideMmountain;
    public Vector3 cubePosition;
    public Vector3 mountainPosition;

    [Header("Wymiary mapy")]
    [SerializeField]
    private float mapWidth = 60;
    [SerializeField]
    private float mapLength = 540;
    [SerializeField]
    private float spawnIceBlockRate = 90;


    void Start()
    {
        IceCubeGenerator();
        SideMountainsGenerator();
    }

    private void IceCubeGenerator()
    {
        GameObject cubeParentObject = new GameObject("iceCubes");   //Wype³nienie planszy bloczkami lodu

        for (var x = 0; x < mapWidth; x += 2)
        {
            for (var z = 0; z < mapLength; z += 2)
            {
                System.Random rnd = new System.Random();
                int randomInt = rnd.Next(0, 100);

                cubePosition = iceCube.transform.position;
                if ((x < 25 || x > 35 || z > 45) && randomInt <= spawnIceBlockRate)
                {
                    GameObject cube = Instantiate(iceCube, new Vector3(cubePosition[0] + x, cubePosition[1], cubePosition[2] + z), Quaternion.identity);
                    cube.transform.parent = cubeParentObject.transform;
                }
            }
        }
    }

    private void SideMountainsGenerator()
    {
        GameObject mountainParentObject = new GameObject("sideMountains");   //Generowanie gór bocznych dla jednego z 3 modu³ów mapy

        for (var z = 180; z < mapLength; z += 180)
        {
            System.Random rnd = new System.Random();
            int randomInt = rnd.Next(0, 5);

            mountainPosition = sideMmountain.transform.position;
            GameObject sideMountain = Instantiate(sideMmountain, new Vector3(mountainPosition[0], mountainPosition[1], mountainPosition[2] + z), Quaternion.identity);
            sideMountain.transform.parent = mountainParentObject.transform;
        }
    }
}

