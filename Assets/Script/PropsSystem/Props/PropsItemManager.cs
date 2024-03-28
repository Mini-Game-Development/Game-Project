using Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PropsItemManager : MonoSingleton<PropsItemManager>
{
    protected override bool _dontDestroyOnLoad  => false;
    const int MAX_PROPS_LIMIT = 99;

    // Start is called before the first frame update
    public TMP_Text CurrentTypeText, CoinText;
    public TMP_Text Backpack, LuckyCat, JewelryHouse;
    public TMP_Text DescribeTitle, DescribeText;
    public Image DescribeImage;

    [Header("Left")]
    public string[] SortTextData;
    public GameObject SortGameObjectBase;
    public GameObject SortGameObjectRoot;
    public List<UIPropsListItem> UIPropsItemList = new List<UIPropsListItem>();
    
    [Header("Right")]
    public Image DisplayImage;
    public Color Color;


    // For Item Swapping
    private PropsUIBase hoveredPropsUI;
    private PropsUIBase pressedPropsUI;
    private ulong displayItemUIID;

    private ulong currentUIID = 0;
    // For type display
    public string currentDisplayItemType = string.Empty;

    public void UpdatePropsItem(string name, int numberOfProps)
    {
        PropsItem[] propsItems = FindPropsOfName(name);
        int remain = numberOfProps;
        foreach(var propsitem in propsItems)
        {
            if (propsitem.CurrentCount+ remain < MAX_PROPS_LIMIT)
            {
                propsitem.CurrentCount += remain;
                propsitem.CurrentCountText.text = propsitem.CurrentCount.ToString();
                UIPropsItemList.Find(item => item.Name == propsitem.itemName).itemCount += remain;
                remain -= remain;
            }
            else if(propsitem.CurrentCount + remain > MAX_PROPS_LIMIT)
            {
                propsitem.CurrentCount = MAX_PROPS_LIMIT;
                propsitem.CurrentCountText.text = propsitem.CurrentCount.ToString();
                UIPropsItemList.Find(item => item.Name == propsitem.itemName).itemCount = MAX_PROPS_LIMIT;

                remain = propsitem.CurrentCount + remain - MAX_PROPS_LIMIT;
            }
        }

        if (remain > 0)
        {
            CreateItem(name, remain);
        }
    }
    public void AddItem(string itemName, int propsCount = 1)
    {
        PropsItem[] propsItems = FindPropsOfName(itemName);
        if(propsItems.Length > 0)
        {
            UpdatePropsItem(itemName, propsCount);
        }
        else
        {
            CreateItem(itemName, propsCount);
        }    
    }
    private void CreateItem(string name, int propsCount = 1)
    {
        if (PropsDataManager.Instance.TryFindDataWithName(name, out PropsData propsData))
        {
            if (SortGameObjectBase)
                SortGameObjectBase.SetActive(true);

            GameObject ItemGameObject = Instantiate(SortGameObjectBase, SortGameObjectBase.transform.position, Quaternion.identity, SortGameObjectRoot.transform);


            PropsItem propsItem;
            propsItem = ItemGameObject.GetComponent<PropsItem>();
            propsItem.UpdateData(propsData, propsCount);
            propsItem.CurrentCount = propsCount;
            propsItem.CurrentCountText.text = propsCount.ToString();
            propsItem.UIID = currentUIID;

            ItemGameObject.transform.rotation = SortGameObjectBase.transform.rotation;

            UIPropsListItem UIItem = new UIPropsListItem();
            UIItem.UIID = currentUIID;
            UIItem.Name = propsData.Name;
            UIItem.itemCount = propsCount;
            UIItem.ItemObject = ItemGameObject;
            UIPropsItemList.Add(UIItem);

            this.currentUIID++;

            if (SortGameObjectBase)
                SortGameObjectBase.SetActive(false);
        }
    }

    public void DeletePropsItem()
    {
        for (int i = 0; i < UIPropsItemList.Count; i++)
        {
            if (UIPropsItemList[i].UIID == displayItemUIID)
            {
                if (UIPropsItemList[i].itemCount <= 1)
                {
                    Destroy(UIPropsItemList[i].ItemObject);
                    UIPropsItemList.Remove(UIPropsItemList[i]);
                }
                else
                {
                    PropsItem item = FindItemByUIID(displayItemUIID);
                    UIPropsItemList[i].itemCount -= 1;
                    item.CurrentCount -= 1;
                    item.CurrentCountText.text = item.CurrentCount.ToString();
                }
            }
        }
    }

    public void ChooseDisplayType(string targetItemType)
    {
        if (targetItemType == currentDisplayItemType)
        {
            for (int i = 0; i < UIPropsItemList.Count; ++i)
            {
                UIPropsItemList[i].ItemObject.SetActive(true);
            }
            currentDisplayItemType = string.Empty;
        }
        else
        {
            for (int i = 0; i < UIPropsItemList.Count; ++i)
            {
                var propsItem = UIPropsItemList[i].ItemObject.GetComponent<PropsItem>();
                if (propsItem.itemType == targetItemType)
                {
                    UIPropsItemList[i].ItemObject.SetActive(true);
                }
                else
                {
                    UIPropsItemList[i].ItemObject.SetActive(false);
                }
            }
            currentDisplayItemType = targetItemType;
        }
        
    }

    public PropsItem[] FindPropsOfName(string targetName)
    {
        var foundDataList = UIPropsItemList.FindAll(item => item.Name == targetName);
        PropsItem[] targetItemList = new PropsItem[foundDataList.Count];
        for (int i = 0; i < foundDataList.Count; ++i)
        {
            targetItemList[i] = foundDataList[i].ItemObject.GetComponent<PropsItem>();
        }

        return targetItemList;
    }
    public PropsItem FindItemByUIID(ulong targetUIID)
    {
        foreach (var item in UIPropsItemList)
        {
            if(item.UIID == targetUIID)
            {
                return item.ItemObject.GetComponent<PropsItem>();
            }
        }
        return null;
    }
    public void UpdateDisplay(ulong UIID)
    {
        PropsItem displayItem = FindItemByUIID(UIID);
        displayItemUIID = displayItem.UIID;
        if (displayItem != null)
        {
            Sprite _tex = Resources.Load<Sprite>("UIPropsListSprite/" + displayItem.ChangeImage);
            DisplayImage.sprite = _tex;//紅區

            Image _colorImg;
            _colorImg = DisplayImage.gameObject.transform.GetChild(0).GetComponent<Image>();
            Color _color;
            ColorUtility.TryParseHtmlString("#" + displayItem.ColorString, out _color);
            _colorImg.color = _color;

            DescribeTitle.text = displayItem.itemName;
            DescribeText.text = displayItem.Describe;
        }
        else
        {
            DisplayImage.sprite = null;
            DescribeTitle.text = string.Empty;
            DescribeText.text = string.Empty;
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
