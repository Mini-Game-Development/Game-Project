using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;
using Game;

public class PropsDataManager : MonoSingleton<PropsDataManager>
{
    protected override bool _dontDestroyOnLoad => false;
    public PropsItemDataList propsDataList;

    public string ChooseItemName;
    // Start is called before the first frame update
    void Start()
    {
        propsDataList = new PropsItemDataList();
        string exclPath = "Assets/Resources/DataTable/PropsData.xlsx";
        string sheetName = "DataPage";
        var excelRowData = ReadExcel(exclPath, sheetName);
        propsDataList.ItemList = ItemDataToJson(excelRowData);
        /*for (int i = 0; i < propsItemDataList.ItemList.Count; i++)
        {
            Debug.Log("propsItemDataList.ItemList[" + i + "].Id:" + propsItemDataList.ItemList[i].Id);
            Debug.Log("propsItemDataList.ItemList[" + i + "].Name:" + propsItemDataList.ItemList[i].Name);
            Debug.Log("propsItemDataList.ItemList[" + i + "].ItemType:" + propsItemDataList.ItemList[i].ItemType);
            Debug.Log("propsItemDataList.ItemList[" + i + "].Describe:" + propsItemDataList.ItemList[i].Describe);
            Debug.Log("propsItemDataList.ItemList[" + i + "].Price:" + propsItemDataList.ItemList[i].Price);
            Debug.Log("propsItemDataList.ItemList[" + i + "].CanUse:" + propsItemDataList.ItemList[i].CanUse);
            Debug.Log("propsItemDataList.ItemList[" + i + "].CanEquipped:" + propsItemDataList.ItemList[i].CanEquipped);
            Debug.Log("propsItemDataList.ItemList[" + i + "].ItemTypeColor:" + propsItemDataList.ItemList[i].ItemTypeColor);
      
        }*/
    }

    //Path檔案位置，excelSheet工作列表
    DataRowCollection ReadExcel(string Path, string excelSheet)
    {
        using (FileStream fileStream = File.Open(Path, FileMode.Open, FileAccess.Read))
        {
            IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
            var result = excelDataReader.AsDataSet();

            return result.Tables[excelSheet].Rows;
        }
    }
    public List<PropsData> ItemDataToJson(DataRowCollection data)
    {
        List<PropsData> DataList = new List<PropsData>();
        PropsData Data;
        // 前三行為說明欄位
        for (int i = 3; i < data.Count; i++)
        {
            Data = new PropsData();
            Data.Id = int.Parse(data[i][0].ToString());
            Data.Name = data[i][1].ToString();
            Data.ItemType = data[i][2].ToString();
            Data.Describe = data[i][3].ToString();
            Data.Price = int.Parse(data[i][4].ToString());
            Data.CanStack = int.Parse(data[i][5].ToString());
            Data.CanUse = bool.Parse(data[i][6].ToString());
            Data.CanEquipped = bool.Parse(data[i][7].ToString());
            Data.ItemTypeColor = data[i][8].ToString();
            DataList.Add(Data);
        }
        return DataList;
    }
    [SerializeField]
    public class PropsItemDataList
    {
        public List<PropsData> ItemList = new List<PropsData>();
    }

}
