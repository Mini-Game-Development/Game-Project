using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelSetting : MonoBehaviour
{
    
    [SerializeField]
    private UIPanelState UIPanelStateManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void DelAndBackPanel()
    {
        UIPanelStateManager.OpenPanel(0);
    }
}
