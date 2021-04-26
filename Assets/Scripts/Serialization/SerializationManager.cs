using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SerializationManager 
{
    public static void Save(object saveData){
        
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.save";

        FileStream file = new FileStream(path, FileMode.Create);

        formatter.Serialize(file, SaveData.current);

        file.Close();
    }

    public static SaveData Load(){
        
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);

            SaveData.current = formatter.Deserialize(file) as SaveData;
            file.Close();

            return SaveData.current;

        } else {
            Debug.Log("Partida guardada no encontrada en la ruta " + path);
            return null;
        }
    }
}
