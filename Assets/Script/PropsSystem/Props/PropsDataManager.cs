using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;
using Game;
using static UnityEngine.GraphicsBuffer;

public class PropsDataManager : MonoSingleton<PropsDataManager>
{
    protected override bool _dontDestroyOnLoad => false;
    public PropsItemDataList propsDataList;
    public string ChooseItemName;
    public TextAsset textFile;
    private List<PropsData> dataList => propsDataList.datas;
    // Start is called before the first frame update
    void Start()
    {
        propsDataList = new PropsItemDataList();
        string exclPath = "Assets/Resources/DataTable/PropsDataTable.txt";
        ReadTable(exclPath);
        Debug.Log(JsonUtility.ToJson(propsDataList));
    }

    //Path檔案位置，excelSheet工作列表
    public void ReadTable(string filePath)
    {
        StreamReader reader = new StreamReader(filePath);
        string fileContents = reader.ReadToEnd();
        propsDataList = JsonUtility.FromJson<PropsItemDataList>(fileContents);
    }
    public List<PropsData> ItemDataToJson(DataRowCollection data)
    {
        List<PropsData> DataList = new List<PropsData>();
        PropsData Data;
        // 前三行為說明欄位
        for (int i = 5; i < data.Count; i++)
        {
            Data = new PropsData();
            Data.propsId = int.Parse(data[i][0].ToString());
            Data.propsName = data[i][1].ToString();
            Data.propsType = data[i][2].ToString();
            Data.description = data[i][3].ToString();
            Data.maxStackLimit = int.Parse(data[i][4].ToString());
            Data.isUsable = bool.Parse(data[i][5].ToString());
            Data.isEquiptable = bool.Parse(data[i][6].ToString());
            Data.ItemTypeColor = data[i][7].ToString();
            Data.ability = data[i][8].ToString();
            Debug.Log(JsonUtility.ToJson(Data));
            DataList.Add(Data);
        }
        return DataList;
    }

    public PropsData GetItemOfID(int targetId)
    {
        return dataList.Find(data => data.propsId == targetId);
    }

    public bool TryGetItemOfID(int targetId, out PropsData data)
    {
        data = dataList.Find(data => data.propsId == targetId);
        return !(data == null);
    }

    [System.Serializable]
    public class PropsItemDataList
    {
        public List<PropsData> datas = new List<PropsData>();
    }

}
