using Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinState : MonoBehaviour
{
    public TMP_Text CoinText;
    public int coinNumber;
    // Start is called before the first frame update
    void Start()
    {
        coinNumber = 1000;
        CoinText.text = coinNumber.ToString();
        GameEventManager.Instance.RegisterEvent(GameEvent.Add_Money, OnAddMoney);
        GameEventManager.Instance.RegisterEvent(GameEvent.Minus_Money, OnMinusMoney);
    }
    private  void OnAddMoney(string param)
    {
        int? number = int.Parse(param);
        if (number != null)
        {
            Plus((int)number);
        }
    }
    private void OnMinusMoney(string param)
    {
        int? number = int.Parse(param);
        if (number != null)
        {
            Minus((int)number);
        }
    }

    public void Minus(int number)
    {
        UpdateCoin(-number);
        CoinText.text = coinNumber.ToString();
        for (int i = 0; i < number; i += 10)
        {
            int newItemId = Random.Range(1, 15);
            PropsItemManager.Instance.AddItem(newItemId);
        }
    }

    public void Plus(int number)
    {
        UpdateCoin(number);
        CoinText.text = coinNumber.ToString();
    }

    private void UpdateCoin(int number)
    {
        var equipmentIds = PropsItemManager.Instance.GetCurrentEquipment();

        foreach (var equipmentId in equipmentIds)
        {
            var data = PropsDataManager.Instance.GetItemOfID(equipmentId);
            if (data == null)
                continue;

            if (!data.ability.Contains("Coin"))
                continue;

            var infos = data.ability.Split('_');
            if (number < 0 && infos[1].Equals("Consumption"))
            {
                number = GetConsumptionAfterReduce(number, infos[2]);
            }
            else if (number > 0 && infos[1].Equals("Aquirement"))
            {
                number = GetAquirementAfterIncrease(number, infos[2]);
            }
        }

        coinNumber += number;
    }

    private int GetConsumptionAfterReduce(int number, string operationString)
    {
        if (operationString.Contains("%"))
        {
            operationString = operationString.Replace("%", "");
            Debug.LogWarning(int.Parse(operationString));
            number = number * (100 - int.Parse(operationString)) / 100;

            Debug.Log(number);
            return number;
        }
        else
        {
            Debug.Log(number);
            return number + Mathf.Abs(int.Parse(operationString));
        }
    }

    private int GetAquirementAfterIncrease(int number, string operationString)
    {
        if (operationString.Contains("%"))
        {
            operationString.Replace("%", "");
            number *= (100 + int.Parse(operationString)) / 100;

            return number;
        }
        else
        {
            return number + int.Parse(operationString);
        }
    }
}
