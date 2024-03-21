using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsManager : MonoBehaviour
{
    [SerializeField]
    private PropsItemManager propsItemManager;
    private List<PropItemData> propsItemList = new List<PropItemData>(); 
    // Start is called before the first frame update
    void Start()
    {
        propsItemList = PropsDataManager.Instance.propsItemDataList.ItemList;
        RoomListinit();
    }

    void RoomListinit()
    {
        for (int i = 0; i < propsItemList.Count; i++)
        {
            AddRoomItem(propsItemList[i].Name);
        }
    }

    public void AddListData(PropItemData data)
    {
        PropsDataManager.Instance.propsItemDataList.ItemList.Add(data);
        AddRoomItem(data.Name);
    }

    public void AddRoomItem(string name)
    {
        propsItemManager.CreateButton(name);
    }
    public void DeleteRoomItem(string name)
    {
        var index = PropsDataManager.Instance.propsItemDataList.ItemList.Find(data => data.Name == name);
        PropsDataManager.Instance.propsItemDataList.ItemList.Remove(index);
        propsItemManager.DeleteButton(name);
    }

    public void UpdateCurrentCount(string state)
    {

    }
}
