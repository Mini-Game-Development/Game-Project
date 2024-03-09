using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TagDisplay : MonoBehaviour
{

    [Header("Tag")]
    //public TMP_Text CurrentTagText;
    public string[] TagTextData;
    //public float[] TagTextDataPosition;
    public GameObject TagGameObjectBase;
    public GameObject TagGameObjectRoot;
    private List<GameObject> TagGameObjectList=new List<GameObject>();
    int oldLength = 1;
    int hideNumber = 0;

    void Start()
    {
        TagGameObjectList.Add(TagGameObjectBase);
    }
    public void TagButtonInit(string[]Data)
    {

        if(Data.Length > oldLength)
        {
            int size = Data.Length - oldLength;
            OpenItem(size);
            for (int i = oldLength; i < Data.Length; i++)
            {
                CreateItem(i);
            }
        }
        else if (Data.Length < oldLength)
        {
            for (int i = Data.Length; i < oldLength; i++)
            {
                HideItem(i);
            }
        }
        
        ChangeText(Data);
    }
    private void CreateItem(int id)
    {
        GameObject ItemGameObject = Instantiate(TagGameObjectBase, TagGameObjectBase.transform.position, Quaternion.identity, TagGameObjectRoot.transform);
        ItemGameObject.transform.rotation = TagGameObjectBase.transform.rotation;
        TagGameObjectList.Add(ItemGameObject);
        oldLength++;
    }
    private void HideItem(int id)
    {
        TagGameObjectList[id].SetActive(false);
        hideNumber++;
    }

    private void OpenItem(int size)
    {
        if(hideNumber!=0)
        {
            int number = hideNumber - size;
            if (number >= 0)
            {
                for (int i = oldLength; i < size + oldLength; i++)
                {
                    TagGameObjectList[i].SetActive(true);
                    hideNumber--;
                }
                oldLength += number;
            }
            else if (number < 0)
            {
                for (int i = oldLength; i < hideNumber + oldLength; i++)
                {
                    TagGameObjectList[i].SetActive(true);
                    hideNumber--;
                }
                oldLength += number;
            }
        }
    }
    private void ChangeText(string[]dataText)
    {
        for(int i=0;i< dataText.Length;i++)
        {
            TMP_Text tagID;
            tagID = TagGameObjectList[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
            tagID.text = dataText[i];

            RectTransform imageID;
            imageID = TagGameObjectList[i].transform.gameObject.GetComponent<RectTransform>();

            ChangeTextOfWidth(tagID, imageID);
        }
       
    }
    public void ChangeTextOfWidth(TMP_Text _tagID, RectTransform _image )
    {
        float number = 5f;
        float textWidth = _tagID.GetComponent<RectTransform>().rect.width + number;
        float textHeight = _tagID.GetComponent<RectTransform>().rect.height + number;
        _image.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth, textHeight);
        //可讓字串在圖片中間顯示，但長度有較長
        //_tagID.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
        /*float width = TextOfWidth(_tagID);
        float imageWidth = width * 25;
        float imageHeight = _image.rect.height;
        _image.sizeDelta = new Vector2(imageWidth, imageHeight);

        float textWidth = (width + 2) * 20;
        float textHeight = _tagID.GetComponent<RectTransform>().rect.height;
        _tagID.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth, textHeight);*/
    }

    public float TextOfWidth(TMP_Text _text)
    {
        float width;
        string data = _text.text.ToString();
        width = data.Length;
        Debug.Log("width:" + width);
        return width;
    }
}
