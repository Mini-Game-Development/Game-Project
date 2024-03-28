using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PropsData
{
    public int Id;
    public string Name;
    public string ItemType;
    public string Describe;
    public int Price;
    public int CanStack;
    public bool CanUse;//可否使用
    public bool CanEquipped;//可否被裝備上
    public string ItemTypeColor;
    public string ChangeImage;
    public int CurrentCount;
}

[System.Serializable]
public enum PropsDataType
{
    coin,           //錢幣
    purse,          //錢帶
    accessories,    //飾品
}
[System.Serializable]
public class UIPropsListItem
{
    public ulong UIID;
    public string Name;
    public int itemCount;
    public GameObject ItemObject;
}