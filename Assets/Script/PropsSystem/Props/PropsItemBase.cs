using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct PropsItemBase
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
public enum PropsItemBaseType
{
    coin,           //錢幣
    purse,          //錢帶
    accessories,    //飾品
}
[System.Serializable]
public struct UIPropsListItem
{
    public string Name;
    public GameObject ItemObject;
}