using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinState : MonoBehaviour
{
    public TMP_Text CoinText;
    public int CoinNumber;
    // Start is called before the first frame update
    void Start()
    {
        CoinNumber = 1000;
        CoinText.text = CoinNumber.ToString();
    }
    public void Minus(int number)
    {
        CoinNumber -= number;
        CoinText.text = CoinNumber.ToString();

        for (int i = 0; i < number; i += 10)
        {
            int newItemId = Random.Range(1, 15);
            string itemName = PropsDataManager.Instance.propsDataList.datas.Find(item => item.Id == newItemId).Name;
            PropsItemManager.Instance.AddItem(itemName);
        }
    }
    public void Plus(int number)
    {
        CoinNumber += number;
        CoinText.text = CoinNumber.ToString();
    }
   
}
