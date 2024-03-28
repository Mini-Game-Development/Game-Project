using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsManager : MonoSingleton<PropsManager>
{
    protected override bool _dontDestroyOnLoad => false;
    [SerializeField]
    private PropsItemManager propsItemManager;
    private List<PropsData> propsList = new List<PropsData>(); 
    // Start is called before the first frame update
    void Start()
    {
        propsList = PropsDataManager.Instance.propsDataList.datas;
        RoomListinit();
    }

    void RoomListinit()
    {
        for (int i = 0; i < propsList.Count; i++)
        {
            propsItemManager.AddItem(propsList[i].Name);
        }
    }

    public void AddProps(PropsData data, int propsCount)
    {
        PropsData oldData = propsList.Find(item => item.Name == data.Name);
        
        if(oldData == null)
        {
            var newData = PropsDataManager.Instance.propsDataList.datas.Find(item => item.Name == data.Name);
            propsList.Add(newData);
        }
        else
        {
            propsItemManager.UpdatePropsItem(oldData.Name, propsCount);
        }
    }

    public void AddProps(string name, int numberOfProps = 1)
    {
        PropsData oldData = propsList.Find(item => item.Name == name);
        
        if (oldData == null)
        {
            var newData = PropsDataManager.Instance.propsDataList.datas.Find(item => item.Name == name);
            propsList.Add(newData);
        }
        else
        {
            propsItemManager.UpdatePropsItem(oldData.Name, numberOfProps);
        }
    }
    public void UseButton()
    {
        DeleteProps();
    }

    public void DeleteProps()
    {
        propsItemManager.DeletePropsItem();
    }

    public void UpdateDisplay(ulong targetUIID)
    {
        propsItemManager.UpdateDisplay(targetUIID);
    }
}
