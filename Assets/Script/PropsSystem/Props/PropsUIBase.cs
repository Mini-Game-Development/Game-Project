using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PropsUIBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PropsItemManager.Instance.RecordPressedItemTransform(this);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        PropsItemManager.Instance.TrySwapItemTransform();
    }
    public void OnPointerEnter(PointerEventData data)
    {
        PropsItemManager.Instance.RecordHoveredItemTransform(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PropsItemManager.Instance.RemoveHoveredItemTransformRecord();
    }
}
