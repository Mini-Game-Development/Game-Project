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
    public PropsItemList propsItemList;

    public string ChooseItemName;
    // Start is called before the first frame update
    void Start()
    {
        propsItemList = new PropsItemList();
        string exclPath = "Assets/Resources/DataTable/PropsData.xlsx";
        string sheetName = "DataPage";
        var excelRowData = ReadExcel(exclPath, sheetName);
        propsItemList.ItemList = ItemDataToJson(excelRowData);
        for (int i = 0; i < propsItemList.ItemList.Count; i++)
        {
            Debug.Log("propsItemList.ItemList[" + i + "].Id:" + propsItemList.ItemList[i].Id);
            Debug.Log("propsItemList.ItemList[" + i + "].Name:" + propsItemList.ItemList[i].Name);
            Debug.Log("propsItemList.ItemList[" + i + "].ItemType:" + propsItemList.ItemList[i].ItemType);
            Debug.Log("propsItemList.ItemList[" + i + "].Describe:" + propsItemList.ItemList[i].Describe);
            Debug.Log("propsItemList.ItemList[" + i + "].Price:" + propsItemList.ItemList[i].Price);
            Debug.Log("propsItemList.ItemList[" + i + "].CanUse:" + propsItemList.ItemList[i].CanUse);
            Debug.Log("propsItemList.ItemList[" + i + "].CanEquipped:" + propsItemList.ItemList[i].CanEquipped);
            Debug.Log("propsItemList.ItemList[" + i + "].ItemTypeColor:" + propsItemList.ItemList[i].ItemTypeColor);
      
        }
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
    public List<PropsItemBase> ItemDataToJson(DataRowCollection data)
    {
        List<PropsItemBase> DataList = new List<PropsItemBase>();
        PropsItemBase Data;
        for (int i = 3; i < data.Count; i++)
        {
            Data = new PropsItemBase();
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
    public class PropsItemList
    {
        public List<PropsItemBase> ItemList = new List<PropsItemBase>();
    }

}
