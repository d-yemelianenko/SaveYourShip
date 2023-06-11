using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour 
{
    private string saveFilePath = "SaveData.txt";
    public Transform PlayerTransform;

    public float posX , posY , posZ ;
    private Vector3 PlayerPosition;

    public void SavePlayerPosition(Vector3 position)
    {
        PlayerPosition = position;
    }

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
            writer.WriteLine(PlayerTransform.rotation.x);
            writer.WriteLine(PlayerTransform.rotation.y);
            writer.WriteLine(PlayerTransform.rotation.z);
            writer.WriteLine(PlayerTransform.rotation.w);
        }

        Debug.Log("Zapisano dane gracza.");
        SceneSaver sceneSaver = GetComponent<SceneSaver>();
    }

    public void LoadPlayer(CharacterStatus playerStatus)
    {
        if (File.Exists(saveFilePath))
        {
            CharacterController characterController = GetComponent<CharacterController>();
            characterController.enabled = false;
            using (StreamReader reader = new StreamReader(saveFilePath))
            {
                SceneLoader sceneLoader = GetComponent<SceneLoader>();

                // Odczytaj parametry postaci
                playerStatus.health = float.Parse(reader.ReadLine());
                playerStatus.stamina = float.Parse(reader.ReadLine());
                playerStatus.cold = float.Parse(reader.ReadLine());
                playerStatus.hunger = float.Parse(reader.ReadLine());

                //if (PlayerPrefs.HasKey("posX") || PlayerPrefs.HasKey("posY") || PlayerPrefs.HasKey("posZ"))
                //{
                    Time.timeScale = 1f;

                    float posX = float.Parse(reader.ReadLine());
                    float posY = float.Parse(reader.ReadLine());
                    float posZ = float.Parse(reader.ReadLine());
                    float rotX = float.Parse(reader.ReadLine());
                    float rotY = float.Parse(reader.ReadLine());
                    float rotZ = float.Parse(reader.ReadLine());
                    float rotW = float.Parse(reader.ReadLine());
                    Debug.Log(rotX);
                    PlayerTransform.position = new Vector3(posX, posY, posZ);
                    Quaternion rotation = new Quaternion(rotX, rotY, rotZ, rotW);
                    PlayerTransform.rotation = rotation;
                    Debug.Log("Wczytano dane gracza.");
                //}
                //else
                //{
                //    Debug.Log("Brak pliku z danymi gracza.");
                //}
            }
            
            characterController.enabled = true;
        }
        else
        {
            Debug.Log("Brak pliku z danymi gracza.");
        }
    }


}

public class SceneSaver : MonoBehaviour
{
    public string saveFilePath = "Assets/SaveData/scene.json";

    public void SaveScene()
    {
        // Pobierz wszystkie obiekty na scenie
        GameObject[] objects = FindObjectsOfType<GameObject>();

        // Utwórz listê dla danych obiektów
        List<ObjectData> objectDataList = new List<ObjectData>();

        // Przeiteruj przez obiekty i zapisz ich dane
        foreach (GameObject obj in objects)
        {
            ObjectData objectData = new ObjectData();
            objectData.name = obj.name;
            objectData.position = obj.transform.position;
            objectData.rotation = obj.transform.rotation;
            // Dodaj inne potrzebne dane obiektu do objectData

            objectDataList.Add(objectData);
        }

        // Zamieñ listê na JSON
        string json = JsonUtility.ToJson(objectDataList, true);

        // Zapisz JSON do pliku
        System.IO.File.WriteAllText(saveFilePath, json);

        Debug.Log("Scene saved!");
    }
}

[System.Serializable]
public class ObjectData
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
}

public class SceneLoader : MonoBehaviour
{
    public string loadFilePath = "Assets/SaveData/scene.json";

    public void LoadScene()
    {
        // Odczytaj zawartoœæ pliku
        string json = System.IO.File.ReadAllText(loadFilePath);

        // Deserializuj dane z formatu JSON
        List<ObjectData> objectDataList = JsonUtility.FromJson<List<ObjectData>>(json);

        // Przeiteruj przez dane obiektów i odtwórz obiekty na scenie
        foreach (ObjectData objectData in objectDataList)
        {
            GameObject obj = new GameObject(objectData.name);
            obj.transform.position = objectData.position;
            obj.transform.rotation = objectData.rotation;
            // Ustaw inne potrzebne dane obiektu na podstawie objectData

            // Dodaj inne skrypty i komponenty do obiektu, jeœli s¹ potrzebne

            // Dodaj obiekt do sceny lub innej kolekcji obiektów w zale¿noœci od potrzeb
        }

        Debug.Log("Scene loaded!");
    }
}