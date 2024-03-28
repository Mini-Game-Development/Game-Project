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
    public bool CanUse;//�i�_�ϥ�
    public bool CanEquipped;//�i�_�Q�˳ƤW
    public string ItemTypeColor;
    public string ChangeImage;
    public int CurrentCount;
}

[System.Serializable]
public enum PropsDataType
{
    coin,           //����
    purse,          //���a
    accessories,    //���~
}
[System.Serializable]
public class UIPropsListItem
{
    public ulong UIID;
    public string Name;
    public int itemCount;
    public GameObject ItemObject;
}