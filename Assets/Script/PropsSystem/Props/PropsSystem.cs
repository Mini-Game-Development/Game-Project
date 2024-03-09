using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropsSystem : MonoBehaviour
{    
    // Start is called before the first frame update
    public TMP_Text CurrentTypeText, CoinText;
    public TMP_Text Backpack, LuckyCat, JewelryHouse;
    public TMP_Text DescribeTitle, DescribeText;
    public Image DescribeImage;

    [Header("Left")]
    public string[] SortTextData;
    public GameObject SortGameObjectBase;
    public GameObject SortGameObjectRoot;
    private List<UIPropsListItem> SortGameObjectList = new List<UIPropsListItem>();

    [Header("Right")]
    public Image DisplayImage;
    public Color Color;
    //public GameObject ItemTypeColor;
    private void Awake()
    {
    }

    public void CreateButton(string name)
    {
        var index = PropsDataManager.Instance.propsItemList.ItemList.Find(data => data.Name == name);
        CreateItem(index);
        SortGameObjectBase.SetActive(false);
    }
    private void CreateItem(PropsItemBase state)
    {
        if (SortGameObjectBase)
            SortGameObjectBase.SetActive(true);
        GameObject ItemGameObject = Instantiate(SortGameObjectBase, SortGameObjectBase.transform.position, Quaternion.identity, SortGameObjectRoot.transform);
        PropsButtonSetting ItemState;
        ItemState = ItemGameObject.GetComponent<PropsButtonSetting>();
        ItemState.ItemGameObjectText = state.Name;
        ItemState.ChangeImage = state.ChangeImage;
        ItemState.ColorString = state.ItemTypeColor;
        ItemState.Describe = state.Describe;
        ItemGameObject.transform.rotation = SortGameObjectBase.transform.rotation;

        UIPropsListItem itemList;
        itemList.Name = state.Name;
        itemList.ItemObject = ItemGameObject;
        SortGameObjectList.Add(itemList);
    }

    public void DelButton(string name)
    {
        for (int i = 0; i < SortGameObjectList.Count; i++)
        {
            if (SortGameObjectList[i].Name == name)
            {
                Destroy(SortGameObjectList[i].ItemObject);
                SortGameObjectList.Remove(SortGameObjectList[i]);
            }
        }
    }

    public void UpdateDisplay(string name, string img, string color, string Describe)
    {
        Sprite _tex = Resources.Load<Sprite>("UIPropsListSprite/" + img);
        DisplayImage.sprite = _tex;//¬õ°Ï

        Image _colorImg;
        _colorImg = DisplayImage.gameObject.transform.GetChild(0).GetComponent<Image>();
        Color _color;
        ColorUtility.TryParseHtmlString("#" + color, out _color);
        _colorImg.color = _color;

        DescribeTitle.text = name;
        DescribeText.text = Describe;
        Debug.Log("DescribeText/Describe" + Describe);
        Debug.Log("DescribeText/text" + DescribeText.text);
    }
   public void UseButton()
    {
        if (PropsDataManager.Instance.ChooseItemName != string.Empty)
        {
            Debug.Log("Choose Room Id:" + PropsDataManager.Instance.ChooseItemName);
        }
    }
}
