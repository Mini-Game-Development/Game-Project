using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTypeButton : MonoBehaviour
{
    public string itemType = string.Empty;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        GetComponentInChildren<TMPro.TMP_Text>().text = itemType;
    }

    public void OnButtonClicked()
    {
        PropsItemManager.Instance.ChooseDisplayType(itemType);
    }
}
