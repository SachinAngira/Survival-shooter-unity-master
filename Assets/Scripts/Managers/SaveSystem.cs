using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    /// <summary>
    /// Saves the Serialized Playerdata in a binary formate into a file 
    /// </summary>
    /// <param name="playerData"></param> PlayerData is a type that has all the properties that we would like to save as in progress.
    public static void SaveTheGame(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    /// <summary>
    /// Deserializes Playerdata from the saved playerData file file 
    /// </summary>
    /// <returns></returns>
    public static PlayerData LoadTheGame()
    {
        string path = Application.persistentDataPath + "/playerData.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
