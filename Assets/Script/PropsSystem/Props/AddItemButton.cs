using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddItemButton : MonoBehaviour
{
    public string itemName;
    public int newItemCount = 1;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);

    }
    public void OnButtonClicked()
    {
        for (int i = 0; i < newItemCount; ++i)
        {
            PropsItemManager.Instance.CreateButton(itemName);
        }
    }
}
