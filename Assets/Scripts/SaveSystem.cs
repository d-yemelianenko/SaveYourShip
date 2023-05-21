using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour 
{
    private string saveFilePath = "SaveData.txt";
    public Transform PlayerTransform;

    public float posX , posY , posZ ;

    private void Update()
    {
        
    }
    public void SavePlayer(CharacterStatus playerStatus)
    {
        using (StreamWriter writer = new StreamWriter(saveFilePath))
        {
            // Zapisz parametry postaci
            writer.WriteLine(playerStatus.health);
            writer.WriteLine(playerStatus.stamina);
            writer.WriteLine(playerStatus.cold);
            writer.WriteLine(playerStatus.hunger);

            // Zapisz pozycjê gracza
            writer.WriteLine(PlayerTransform.position.x);
            writer.WriteLine(PlayerTransform.position.y);
            writer.WriteLine(PlayerTransform.position.z);

        }

        Debug.Log("Zapisano dane gracza.");
    }

    public void LoadPlayer(CharacterStatus playerStatus)
    {
        if (File.Exists(saveFilePath))
        {
            using (StreamReader reader = new StreamReader(saveFilePath))
            {
                // Odczytaj parametry postaci
                playerStatus.health = float.Parse(reader.ReadLine());
                playerStatus.stamina = float.Parse(reader.ReadLine());
                playerStatus.cold = float.Parse(reader.ReadLine());
                playerStatus.hunger = float.Parse(reader.ReadLine());

                       if (PlayerPrefs.HasKey("posX") || PlayerPrefs.HasKey("posY") || PlayerPrefs.HasKey("posZ"))
                    {
                        
                        Time.timeScale = 1f;

                    float posX = float.Parse(reader.ReadLine());
                    float posY = float.Parse(reader.ReadLine());
                    float posZ = float.Parse(reader.ReadLine());
                    Debug.Log(posX);
                    PlayerTransform.position = new Vector3(posX, posY, posZ);
                }
                else
                {
                    Debug.Log("Brak pliku z danymi gracza.");
                }
            }

            Debug.Log("Wczytano dane gracza.");
        }
        else
        {
            Debug.Log("Brak pliku z danymi gracza.");
        }
    }


}
   


