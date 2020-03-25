using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class LevelSave
{
    public static void SaveLevel(Level lev)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(GetPath(), FileMode.Create);
        LevelData data = new LevelData(lev);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadLevel()
    {
        if (File.Exists(GetPath()))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(GetPath(), FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("File not found at: " + GetPath());
            return null;
        }
    }

    public static string GetPath()
    {
        return Application.persistentDataPath + "/level.bouncy";
    }
}
