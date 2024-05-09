using UnityEngine;
using Game;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Drawing;
using UnityEngine.EventSystems;

public class PropsItem : PropsUIBase
{
    public int itemIndex = -1;
    public int propsId = -1;
    protected int currentCount = 0;
    public int CurrentCount => currentCount;
    [SerializeField] private TMP_Text CurrentCountText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image colorImage;
    private string ChangeImage;
    // Start is called before the first frame update

    void Start()
    {
    }

    public override string GetDataString()
    {
        Props propsData = new Props()
        {
            propsId = currentCount > 0 ? this.propsId : 0,
            CurrentCount = currentCount,
        };
        return JsonUtility.ToJson(propsData);
    }

    public int GetCurrentCount()
    {
        return currentCount;
    }
    public int MaxStackLimit()
    {
        var data = PropsDataManager.Instance.GetItemOfID(propsId);
        return data != null ? data.maxStackLimit : -1;
    }

    public override bool TrySetData(string dataString)
    {
       var props = JsonUtility.FromJson<Props>(dataString);
       
        if (props != null && props.CurrentCount > 0) 
        { 
            propsId = props.propsId;
            currentCount = props.CurrentCount;
        }
        else
        {
            propsId = 0;
            currentCount = 0;
        }
        
        RefreshUI();
        return true;
    }

    public void UpdateData(int newPropsId, int count)
    {
        this.propsId = newPropsId;
        currentCount += count;
        RefreshUI();
    }

    protected virtual void RefreshUI()
    {
        //Debug.Log($"Index = {itemIndex}, propsId = {propsId}, currentCount = {currentCount}");
        if (currentCount > 0)
        {
            itemImage.gameObject.SetActive(true);
            var data = PropsDataManager.Instance.GetItemOfID(propsId);
            if (data != null)
            {
                CurrentCountText.text = currentCount.ToString();
                string ColorString = data.ItemTypeColor;

                UnityEngine.Color _color;
                ColorUtility.TryParseHtmlString("#" + ColorString, out _color);
                colorImage.color = _color;
            }
        }
        else
        {
            CurrentCountText.text = string.Empty;
            itemImage.gameObject.SetActive(false);
            //Debug.Log("RefreshUI");
        }
        
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        var data = PropsDataManager.Instance.GetItemOfID(propsId);
        if (data == null)
            return;

        if (currentCount > 0)
        {
            PropsItemManager.Instance.UpdateDisplay(propsId);
        }
        //PropsDataManager.Instance.ChooseItemName = ItemGameObjectText.ToString();
    }

    public override void Action()
    {
        base.Action();
        var data = PropsDataManager.Instance.GetItemOfID(propsId);
        if (data == null)
            return;
        if (!data.isUsable)
            return;

        string[] infos = data.ability.Split("_");
        if (infos[0] == "Coin")
            ChangeMoney(infos[1], infos[2]);

        PropsItemManager.Instance.DeleteProps(itemIndex);
    }

    private void ChangeMoney(string acitonString, string numberString)
    {
        switch(acitonString)
        {
            case "Count":
                GameEventManager.Instance.SendEvent(GameEvent.Add_Money, numberString);
                break;
        }
    }
}

