using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PropsData
{
    public int propsId;
    public string propsName;
    public string propsType;
    public string description;
    public int maxStackLimit;
    public bool isUsable;//�i�_�ϥ�
    public bool isEquiptable;//�i�_�Q�˳ƤW
    public string ItemTypeColor;
    public string ability;
}

[System.Serializable]
public class PropsBasicInfo
{
    
}

[System.Serializable]
public class Props
{
    public int CurrentCount;
    public int propsId;
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
    public string name;
    public int itemCount;
    public GameObject ItemObject;
}