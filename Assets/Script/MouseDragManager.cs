using Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseDragManager : MonoSingleton<MouseDragManager>
{
    protected override bool _dontDestroyOnLoad => false;
    PropsUIBase hoveredUICache;
    PropsUIBase pressedUICache;

    [SerializeField] Transform draggingDisplayRoot;
    Image draggingImage;
    TMPro.TMP_Text descriptionText;
    private double pressTime;

    private void Start()
    {
        draggingImage = draggingDisplayRoot.GetComponentInChildren<Image>();
        descriptionText = draggingDisplayRoot.GetComponentInChildren<TMPro.TMP_Text>();

        draggingImage.enabled = false;
        draggingImage.raycastTarget = false;
    }
    private void Update()
    {
        SetDraggingImagePos((Input.mousePosition));
    }

    
    public void SetHoveredUI(PropsUIBase hoveredUI)
    {
        hoveredUICache = hoveredUI;
        
    }

    public void ReleaseHoverdUI()
    {
        hoveredUICache = null;
    }

    public void SetPressedUI(PropsUIBase pressedUI)
    {
        pressedUICache = pressedUI;
    }

    public void ReleasePressedUI()
    {
        if (hoveredUICache == null)
        { 
            pressedUICache = null;
            draggingImage.enabled = false;
        }
        else if(pressedUICache != null)
        {
            string dataString = hoveredUICache.GetDataString();
            if (hoveredUICache.TrySetData(pressedUICache.GetDataString()) && pressedUICache.TrySetData(dataString))
            {
                pressedUICache = null;
                draggingImage.enabled = false;
            }
        }
    }

    public void SetDraggingImagePos(Vector3 newCursorPos)
    {
        draggingDisplayRoot.position = newCursorPos;
    }

    public PropsUIBase GetPressedUI()
    {
        return pressedUICache;
    }
}

public enum MouseState
{
    None = 0,
    Idel = 1 << 0,
    Hover = 1 << 1,
    Press = 1 << 2,
    Hold = 1 << 3,
}