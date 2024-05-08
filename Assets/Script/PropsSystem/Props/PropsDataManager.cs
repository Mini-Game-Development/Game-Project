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
