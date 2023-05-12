using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PlayerData 
{
    public float health;
    public float stamina;
    public float cold;
    public float hunger;
    public float[] position;

    [SerializeField]
    private GameObject playerObj;

    public PlayerData(CharacterStatus player)
    {
        health = player.health;
        stamina = player.stamina;
        cold = player.cold;
        hunger = player.hunger;

        position = new float[3];
        position[0] = playerObj.transform.position.x;
        position[1] = playerObj.transform.position.y;
        position[2] = playerObj.transform.position.z;
    }
    
}
