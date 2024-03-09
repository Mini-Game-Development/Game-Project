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
    public bool CanUse;//�i�_�ϥ�
    public bool CanEquipped;//�i�_�Q�˳ƤW
    public string ItemTypeColor;
    public string ChangeImage;
    public int CurrentCount;
}

[System.Serializable]
public enum PropsItemBaseType
{
    coin,           //����
    purse,          //���a
    accessories,    //���~
}
[System.Serializable]
public struct UIPropsListItem
{
    public string Name;
    public GameObject ItemObject;
}