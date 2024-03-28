using UnityEngine;
using Game;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Drawing;

public class PropsItem : PropsUIBase
{
    public ulong UIID;
    public string itemName;
    public TMP_Text CurrentCountText;
    public Image itemImage;
    public Image colorImage;
    public int CurrentCount;
    public string Describe;
    public string itemType;
    public string ChangeImage;
    public string ColorString;
    // Start is called before the first frame update

    void Start()
    {
        TMP_Text SortText;
        SortText = CurrentCountText.GetComponent<TMP_Text>();
        SortText.text = CurrentCount.ToString();

        UnityEngine.Color _color;
        ColorUtility.TryParseHtmlString("#" + ColorString, out _color);
        colorImage.color = _color;
    }

    public void UpdateData(PropsData data, int propsCount)
    {
        itemType = data.ItemType;
        itemName = data.Name;
        ChangeImage = data.ChangeImage;
        ColorString = data.ItemTypeColor;
        Describe = data.Describe;

        CurrentCount = propsCount;
        CurrentCountText.text = CurrentCount.ToString();
    }

    public void getCurrentSortButtonActive()
    {
        PropsItemManager.Instance.UpdateDisplay(UIID);
        //PropsDataManager.Instance.ChooseItemName = ItemGameObjectText.ToString();
    }
}

