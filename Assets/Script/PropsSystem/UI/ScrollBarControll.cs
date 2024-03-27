using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollBarControll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ScrollRect scrollRect;

    private void Start()
    {
        scrollRect.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        scrollRect.enabled = true; //(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        scrollRect.enabled = false;
    }
}
