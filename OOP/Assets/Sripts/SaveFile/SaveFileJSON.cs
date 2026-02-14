using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveFileJSON : MonoBehaviour
{
    private string SavePath => Path.Combine(Application.persistentDataPath, "pets.json");

    public void SaveFile(DataFilePet file)
    {
        try
        {
            string json = JsonUtility.ToJson(file, true);
            File.WriteAllText(SavePath, json);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveSystem] Failed to save file: {e.Message}");
        }
    }

    public DataFilePet Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("[SaveSystem] Save file not found. Creating new.");
            return CreateNewData();
        }

        try
        {
            string json = File.ReadAllText(SavePath);
            DataFilePet data = JsonUtility.FromJson<DataFilePet>(json);

            if (data == null) return CreateNewData();
            if (data.pets == null) data.pets = new List<PetFile>();

            return data;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveSystem] Failed to load file (File corrupted?): {e.Message}");
            return CreateNewData();
        }
    }

    private DataFilePet CreateNewData()
    {
        return new DataFilePet
        {
            pets = new List<PetFile>(),
            activePet = null 
        };
    }
}

[System.Serializable]
public class DataFilePet
{
    public PetFile activePet;
    public List<PetFile> pets;
}

[System.Serializable]
public class PetFile
{
    public string namePet;
    public int rarity;
}