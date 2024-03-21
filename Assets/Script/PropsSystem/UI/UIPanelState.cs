using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIPanelState : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] PanelList;
    public int[] numberList;
    void Start()
    {
        PanelInit();
        OpenPanel(0);
        
    }
    public void PanelInit()
    {
    }

    public void OpenPanel(int ID)
    {
        for (int i = 0; i < PanelList.Length; i++)
        {
            if (i != ID && ID !=-1)
                PanelList[i].SetActive(false);
            else
                PanelList[i].SetActive(true);
        }
      
    }

}
