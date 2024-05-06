using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddItemButton : MonoBehaviour
{
    public int itemId = 1;
    public int newItemCount = 1;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);

    }
    public void OnButtonClicked()
    {
        PropsItemManager.Instance.AddItem(itemId, newItemCount);
    }
}
