using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsManager : MonoSingleton<PropsManager>
{
    protected override bool _dontDestroyOnLoad => false;
    [SerializeField]
    private PropsItemManager propsItemManager;
    private List<PropsData> propsItemList = new List<PropsData>(); 
    // Start is called before the first frame update
    void Start()
    {
        propsItemList = PropsDataManager.Instance.propsDataList.ItemList;
        RoomListinit();
    }

    void RoomListinit()
    {
        for (int i = 0; i < propsItemList.Count; i++)
        {
            propsItemManager.CreateButton(propsItemList[i].Name);
        }
    }

    public void AddProps(PropsData data, int propsCount)
    {
        PropsData oldData = propsItemList.Find(item => item.Name == data.Name);
        
        if(oldData == null)
        {
            var newData = PropsDataManager.Instance.propsDataList.ItemList.Find(item => item.Name == data.Name);
            propsItemList.Add(newData);
            propsItemManager.CreateButton(data.Name, propsCount);
        }
        else
        {
            propsItemManager.UpdatePropsItem(oldData.Name, propsCount);
        }
    }

    public void AddProps(string name, int numberOfProps = 1)
    {
        PropsData oldData = propsItemList.Find(item => item.Name == name);
        
        if (oldData == null)
        {
            var newData = PropsDataManager.Instance.propsDataList.ItemList.Find(item => item.Name == name);
            propsItemList.Add(newData);
            propsItemManager.CreateButton(name, numberOfProps);
        }
        else
        {
            propsItemManager.UpdatePropsItem(oldData.Name, numberOfProps);
        }
    }
    public void DeleteProps(string name)
    {
        var index = PropsDataManager.Instance.propsDataList.ItemList.Find(data => data.Name == name);
        PropsDataManager.Instance.propsDataList.ItemList.Remove(index);
        propsItemManager.DeleteButton(name);
    }
}
