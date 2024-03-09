using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsManager : MonoBehaviour
{
    [SerializeField]
    private PropsSystem UIPropsListStateManager;
    // Start is called before the first frame update
    void Start()
    {

        RoomListinit();
    }

    void RoomListinit()
    {
        for (int i = 0; i < PropsDataManager.Instance.propsItemList.ItemList.Count; i++)
        {
            AddRoomItem(PropsDataManager.Instance.propsItemList.ItemList[i].Name);
        }
    }

    public void AddListData(PropsItemBase data)
    {
        PropsDataManager.Instance.propsItemList.ItemList.Add(data);
        AddRoomItem(data.Name);
    }

    public void AddRoomItem(string name)
    {
        UIPropsListStateManager.CreateButton(name);
    }
    public void DelRoomItem(string name)
    {
        var index = PropsDataManager.Instance.propsItemList.ItemList.Find(data => data.Name == name);
        PropsDataManager.Instance.propsItemList.ItemList.Remove(index);
        UIPropsListStateManager.DelButton(name);
    }

    public void UpdateCurrentCount(string state)
    {

    }
}
