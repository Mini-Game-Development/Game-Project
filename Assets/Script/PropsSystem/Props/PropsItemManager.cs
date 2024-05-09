using Game;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropsItemManager : MonoSingleton<PropsItemManager>
{
    protected override bool _dontDestroyOnLoad => true;
    const int MAX_ITEM_COUNT = 30;
    // Start is called before the first frame update
    public TMP_Text CurrentTypeText, CoinText;
    public TMP_Text Backpack, LuckyCat, JewelryHouse;
    public TMP_Text DescribeTitle, DescribeText;
    public Image DescribeImage;

    [Header("Left")]
    public string[] SortTextData;
    public GameObject SortGameObjectBase;
    public GameObject SortGameObjectRoot;
    
    [Header("Right")]
    public Image DisplayImage;
    public Color Color;

    [SerializeField] private List<PropsItem> equipmentList = new List<PropsItem>();
    [SerializeField] private List<Props> propsRecord = new List<Props>();
    private List<PropsItem> propsItemList = new List<PropsItem>();
    
    // For Item Swapping
    

    // For type display
    public string currentDisplayItemType = string.Empty;

    private void Start()
    {
        for(int i = 0; i < MAX_ITEM_COUNT; ++i)
        {
            GameObject ItemGameObject = Instantiate(SortGameObjectBase, SortGameObjectBase.transform.position, Quaternion.identity, SortGameObjectRoot.transform);
            var item = ItemGameObject.GetComponent<PropsItem>();
            item.UpdateData(-1, 0);
            item.itemIndex = i;
            propsItemList.Add(item);
        }
    }

    public void AddItem(int newPropsId, int numberOfProps = 1)
    {
        var propsData = PropsDataManager.Instance.GetItemOfID(newPropsId);
        if (propsData == null)
            return;

        
        int remain = numberOfProps;
        int maxStackLimit = propsData.maxStackLimit;

        if (FindAllPropsItemOfId(newPropsId, out PropsItem[] itemsWithSameId))
        {
            foreach(var item in itemsWithSameId)
            {
                if (item.CurrentCount == item.MaxStackLimit())
                {
                    continue;
                }
                else if(item.CurrentCount + remain <= item.MaxStackLimit())
                {
                    item.UpdateData(newPropsId, remain);
                    remain -= remain;
                }
                else if(item.CurrentCount + remain > item.MaxStackLimit())
                {
                    remain = item.CurrentCount + remain - item.MaxStackLimit();
                    item.UpdateData(newPropsId, item.MaxStackLimit()- item.CurrentCount);
                }
            }
        }
        while(remain > 0 && TryGetEmptyPropsItem(out PropsItem emptyItem))
        {
            if (remain > maxStackLimit)
            {
                remain -= maxStackLimit;
                emptyItem.UpdateData(newPropsId, maxStackLimit);
            }
            else
            {
                emptyItem.UpdateData(newPropsId, remain);
                break;
            }
        }
        if(!TryGetEmptyPropsItem(out PropsItem empty))
        {
            Debug.LogError("Not Enough Space");
        }
    }

    private bool FindAllPropsItemOfId(int targetId,out PropsItem[] targetProps)
    {
        List<PropsItem> tempList = propsItemList.FindAll(item => item.propsId == targetId);

        if(tempList?.Count > 0)
        {
            targetProps = tempList.ToArray();
            return true;
        }
        else 
        {
            targetProps = null;
            return false; 
        }
    }

    public void DeleteProps(int itemIndex, int numberOfProps = 1)
    {
        if (propsItemList[itemIndex].CurrentCount < numberOfProps)
            return;

        propsItemList[itemIndex].UpdateData(propsItemList[itemIndex].propsId, -numberOfProps);
    }

    private bool TryGetEmptyPropsItem(out PropsItem emptyItem)
    {
        foreach(var propsItem in propsItemList)
        {
            if(propsItem.CurrentCount == 0)
            {
                emptyItem = propsItem;
                return true;
            }
        }

        emptyItem = null;
        return false;
    }

    public int[] GetCurrentEquipment()
    {
        int[] propsIdList = new int[equipmentList.Count];
        for(int i = 0; i < equipmentList.Count; ++i)
        {
            propsIdList[i] = equipmentList[i].propsId;
        }
        return propsIdList;
    }

    private void AddPropsRecord(int propsId, int numbersToAdd)
    {
        Props props = propsRecord.Find(target => target.propsId == propsId);
        if(props != null)
        {
            props.CurrentCount += numbersToAdd;
        }
    }

    private void RemovePropRecord(int propsId, int numbersToRemove)
    {
        Props props = propsRecord.Find(target => target.propsId == propsId);
        if (props != null)
        {
            props.CurrentCount -= numbersToRemove;
        }
    }

    public void ChooseDisplayType(string targetItemType)
    {
        if (targetItemType == currentDisplayItemType)
        {
            foreach (var item in propsItemList)
            {
                item.gameObject.SetActive(true);
            }
        }
        else
        {
            currentDisplayItemType = targetItemType;
            foreach (var item in propsItemList)
            {
                var data = JsonUtility.FromJson<PropsData>(item.GetDataString());
                if (data.propsType == targetItemType)
                {
                    item.gameObject.SetActive(true);
                }
                else
                {
                    item.gameObject.SetActive(false);
                }
            }
        }
    }
    
    public void UpdateDisplay(int itemId)
    {
        var displayData = PropsDataManager.Instance.GetItemOfID(itemId);
        if (displayData != null)
        {
            /*Sprite _tex = Resources.Load<Sprite>("UIPropsListSprite/" + displayData.ChangeImage);
            DisplayImage.sprite = _tex;//¬õ°Ï*/

            Image _colorImg;
            _colorImg = DisplayImage.gameObject.transform.GetChild(0).GetComponent<Image>();
            Color _color;
            ColorUtility.TryParseHtmlString("#" + displayData.ItemTypeColor, out _color);
            _colorImg.color = _color;

            DescribeTitle.text = displayData.propsName;
            DescribeText.text = displayData.description;
        }
        else
        {
            DisplayImage.sprite = null;
            DescribeTitle.text = string.Empty;
            DescribeText.text = string.Empty;
        }
    }
}
