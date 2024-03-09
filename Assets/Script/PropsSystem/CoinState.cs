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
    }
    public void Plus(int number)
    {
        CoinNumber += number;
        CoinText.text = CoinNumber.ToString();
    }
   
}
