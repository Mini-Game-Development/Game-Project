using Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class PropsItemManager : MonoSingleton<PropsItemManager>
{
    protected override bool _dontDestroyOnLoad  => false;
    // Start is called before the first frame update
    public TMP_Text CurrentTypeText, CoinText;
    public TMP_Text Backpack, LuckyCat, JewelryHouse;
    public TMP_Text DescribeTitle, DescribeText;
    public Image DescribeImage;

    [Header("Left")]
    public string[] SortTextData;
    public GameObject SortGameObjectBase;
    public GameObject SortGameObjectRoot;
    public List<UIPropsListItem> SortGameObjectList = new List<UIPropsListItem>();

    [Header("Right")]
    public Image DisplayImage;
    public Color Color;


    // For Item Swapping
    private PropsUIBase hoveredPropsUI;
    private PropsUIBase pressedPropsUI;

    // For type display
    public string currentDisplayItemType = string.Empty;

    public void UpdatePropsItem(string name, int numberOfProps)
    {
        PropsItem item = FindPropsItemByName(name);

        if (item != null)
        {
            Debug.Log("w");
            item.CurrentCount += numberOfProps;
            item.CurrentCountText.text = item.CurrentCount.ToString();
        }
    }

    public void CreateButton(string name, int count = 1)
    {
        var itemData = PropsDataManager.Instance.propsDataList.ItemList.Find(data => data.Name == name);
        if (itemData != null)
        {
            Debug.Log("W3W");
            CreateItem(itemData, count);
            SortGameObjectBase.SetActive(false);
        }
    }
    private void CreateItem(PropsData itemData, int propsCount = 1)
    {
        if (SortGameObjectBase)
            SortGameObjectBase.SetActive(true);
        
        GameObject ItemGameObject = Instantiate(SortGameObjectBase, SortGameObjectBase.transform.position, Quaternion.identity, SortGameObjectRoot.transform);
        
        PropsItem propsItem;
        propsItem = ItemGameObject.GetComponent<PropsItem>();
        propsItem.UpdateData(itemData, propsCount);
        propsItem.CurrentCount = propsCount;
        propsItem.CurrentCountText.text = propsCount.ToString();
        ItemGameObject.transform.rotation = SortGameObjectBase.transform.rotation;

        UIPropsListItem itemList = new UIPropsListItem();
        itemList.Name = itemData.Name;
        itemList.itemCount = propsCount;
        itemList.ItemObject = ItemGameObject;
        SortGameObjectList.Add(itemList);
    }

    public void DeleteButton(string name)
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

    public void ChooseDisplayType(string targetItemType)
    {
        if (targetItemType == currentDisplayItemType)
        {
            for (int i = 0; i < SortGameObjectList.Count; ++i)
            {
                SortGameObjectList[i].ItemObject.SetActive(true);
            }
            currentDisplayItemType = string.Empty;
        }
        else
        {
            for (int i = 0; i < SortGameObjectList.Count; ++i)
            {
                var propsItem = SortGameObjectList[i].ItemObject.GetComponent<PropsItem>();
                if (propsItem.itemType == targetItemType)
                {
                    SortGameObjectList[i].ItemObject.SetActive(true);
                }
                else
                {
                    SortGameObjectList[i].ItemObject.SetActive(false);
                }
            }
            currentDisplayItemType = targetItemType;
        }
        
    }

    public PropsItem FindPropsItemByName(string name)
    {
        for(int i = 0; i < SortGameObjectList.Count; ++i)
        {
            if (SortGameObjectList[i].Name == name)
            {
                return SortGameObjectList[i].ItemObject.GetComponent<PropsItem>();
            }
        }

        return null;
    }

    public void UpdateDisplay(string name, string img, string color, string Describe)
    {
        Sprite _tex = Resources.Load<Sprite>("UIPropsListSprite/" + img);
        DisplayImage.sprite = _tex;//紅區

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

    public void RecordHoveredItemTransform(PropsUIBase newUI)
    {
        hoveredPropsUI = newUI;
    }

    public void RecordPressedItemTransform(PropsUIBase newUI)
    {
        pressedPropsUI = newUI;
    }

    public void RemoveHoveredItemTransformRecord()
    {
        hoveredPropsUI = null;
    }

    public void TrySwapItemTransform()
    {
        if (hoveredPropsUI && pressedPropsUI)
        {
            SwapSiblingIndex(hoveredPropsUI.transform, pressedPropsUI.transform);
            Debug.Log("Swap Index");
        }
    }

    public void SwapSiblingIndex(Transform sibling1, Transform sibling2)
    {
        int index1 = sibling1.GetSiblingIndex();
        int index2 = sibling2.GetSiblingIndex();

        // 交換索引位置
        sibling1.SetSiblingIndex(index2);
        sibling2.SetSiblingIndex(index1);
    }
}
