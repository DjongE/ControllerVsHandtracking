using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    public string dataName;
    private List<InteractionData> _datas = new List<InteractionData>();

    public void AddData(InteractionData data)
    {
        _datas.Add(data);
    }

    public void GenerateJSON()
    {
        string jsonData = JsonUtility.ToJson(_datas, true);
        string filePath = Application.persistentDataPath + "/dataList.json";
        File.WriteAllText(filePath, jsonData);
    }
}
