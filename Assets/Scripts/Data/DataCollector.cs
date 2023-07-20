using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


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

    public void AddData(InteractionData data)
    {
        datas.list.Add(data);
    }

    public void GenerateJSON(string inputName)
    {
        string jsonData = JsonUtility.ToJson(datas, true);
        string filePath = Application.persistentDataPath + "/" + inputName + "dataList.json";
        Debug.Log(filePath + datas.list.Count);
        Debug.Log(jsonData);
        File.WriteAllText(filePath, jsonData);
    }
}
