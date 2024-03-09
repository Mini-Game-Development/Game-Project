using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LotteryState : MonoBehaviour
{
    public TMP_Text LotteryOne, LotteryMany;
    public string CurrentPage;
    [SerializeField]
    private CoinState CoinStateManager;
    [SerializeField]
    private UIPanelState UIPanelStateManager;
    // Start is called before the first frame update
    void Start()
    {
       
    }

   
    public void Minus(int number)
    {
        Debug.Log("Minus/number: " + number);
        CoinStateManager.Minus(number);
        //UIPanelStateManager.OpenPanel(-1);
        //LootBoxManager.OpenBox();
    }
}
