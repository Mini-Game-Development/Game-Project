using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropsUIBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    public Image propsImage;
    private double clickTime = 0;
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        MouseDragManager.Instance.SetPressedUI(this);
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        MouseDragManager.Instance.ReleasePressedUI();
    }
    public virtual void OnPointerEnter(PointerEventData data)
    {
        MouseDragManager.Instance.SetHoveredUI(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        MouseDragManager.Instance.ReleaseHoverdUI();
        clickTime = 0;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if(clickTime > 0 && clickTime + 0.3f >= Time.time)
        {
            clickTime = 0;
            Action();
        }
        else if (clickTime < Time.time)
        {
            clickTime = Time.time;
        }
        else
        {
            clickTime = 0;
        }
    }

    public virtual bool TrySetData(string dataString)
    {
        return true;
    }

    public virtual string GetDataString()
    {
        return string.Empty;
    }

    public virtual void Action()
    {
        Debug.Log("Action");
    }
}
