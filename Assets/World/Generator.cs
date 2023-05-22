using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject iceCube;
    public GameObject [] sideMountain;
    public GameObject[] iceMountain;
    public Vector3 cubePosition;
    public Vector3 mountainPosition;

    [Header("Wymiary mapy")]
    [SerializeField]
    private float mapWidth = 40;
    [SerializeField]
    private float mapLength = 360;
    [SerializeField]
    private int spawnIceBlockRate = 80;
    [SerializeField]
    private int spawnIceMountainRate = 1;
    [SerializeField]
    private int spawnIceMountainBufor = 10;


    void Start()
    {
        IceCubeGenerator();
        SideMountainsGenerator();
    }

    private void IceCubeGenerator()   //Wype³nienie planszy bloczkami lodu i lodowymi górami
    {
        GameObject cubeParentObject = new GameObject("iceCubes");
        GameObject iceMountainsParentObject = new GameObject("iceMountains");

        int i = 0, j = 0; // j - tworzy przerwy w generowaniu gór lodowych,
        for (var z = 0; z < mapLength; z += 3)
        {
            for (var x = 0; x < mapWidth; x += 3)
            {
                System.Random rnd = new System.Random();
                int randomSpawnCube = rnd.Next(0, 100);
                int randomSpawnIceMountain = rnd.Next(1, 1000);

                cubePosition = iceCube.transform.position;
                if ((x < 24 || x > 36 || z > 46) && randomSpawnCube <= spawnIceBlockRate)   // Wyrwa na statek
                {
                    if((z > 180 && z < mapLength - 20) && (x > 6 && x < 54) && j <= 0)
                    {
                        if (randomSpawnIceMountain <= spawnIceMountainRate) // Generowanie gór lodowych
                        {
                            int randomIceMountain = rnd.Next(0, iceMountain.Length);
                            iceMountain[i] = Instantiate(iceMountain[randomIceMountain], new Vector3(cubePosition[0] + x, cubePosition[1], cubePosition[2] + z), Quaternion.identity);
                            iceMountain[i].transform.parent = iceMountainsParentObject.transform;
                            
                            MeshCollider collider = iceMountain[i].GetComponent<MeshCollider>();    // Dodaj Collider w trybie Convex
                            if (collider == null)
                                collider = iceMountain[i].AddComponent<MeshCollider>();
                            collider.convex = true;

                            Rigidbody rigidbody = iceMountain[i].GetComponent<Rigidbody>();    // Dodaj Collider w trybie isKinematic
                            if (rigidbody == null)
                                rigidbody = iceMountain[i].AddComponent<Rigidbody>();
                            rigidbody.useGravity = false;
                            rigidbody.isKinematic = true;

                            iceMountain[i].tag = "IceMountain";
                            j = spawnIceMountainBufor;
                        }
                    }
                    GameObject cube = Instantiate(iceCube, new Vector3(cubePosition[0] + x, cubePosition[1], cubePosition[2] + z), Quaternion.identity);
                    cube.transform.parent = cubeParentObject.transform;
                }

            }
            if (j > 0) j--;
        }
    }

    private void SideMountainsGenerator()
    {
        GameObject mountainParentObject = new GameObject("sideMountains");   //Generowanie gór bocznych dla jednego z 3 modu³ów mapy

        int i = 0;
        for (var z = 180; z < mapLength; z += 180)
        {
            i++;
            System.Random rnd = new System.Random();
            mountainPosition = sideMountain[0].transform.position;
            int randomInt = rnd.Next(0, sideMountain.Length);
            sideMountain[i] = Instantiate(sideMountain[randomInt], new Vector3(mountainPosition[0], mountainPosition[1], mountainPosition[2] + z), Quaternion.identity);
            sideMountain[i].transform.parent = mountainParentObject.transform;
        }
    }
}

