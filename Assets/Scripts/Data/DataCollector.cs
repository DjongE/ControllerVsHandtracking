using System.Collections.Generic;
using System.IO;
using UnityEngine;

/**
 * Die Liste muss serialisiert sein, damit sie in eine Datei gespeichert werden kann
 */
[System.Serializable]
public class SerializableList<T> {
    public List<T> list;
}

public class DataCollector : MonoBehaviour
{
    public string dataName;
    
    [SerializeField]
    private SerializableList<InteractionData> datas = new SerializableList<InteractionData>();

    private void Start()
    {
        if (datas == null)
            datas = new SerializableList<InteractionData>();
    }

    /**
     * Add InteractionData object to the datas list
     */
    public void AddData(InteractionData data)
    {
        datas.list.Add(data);
    }

    /**
     * Formatting the data list into json format
     * and save the file to the user library folder
     */
    public void GenerateJSON(string inputName)
    {
        string jsonData = JsonUtility.ToJson(datas, true);
        string filePath = Application.persistentDataPath + "/" + inputName + "dataList.json";
        Debug.Log(filePath + datas.list.Count);
        Debug.Log(jsonData);
        File.WriteAllText(filePath, jsonData);
    }
}
