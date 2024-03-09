using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LotteryBoxState : MonoBehaviour
{
    [SerializeField] LotteryBoxItem[] m_Lottery;
    [SerializeField] Transform m_LotteryObjectRoot;
    [SerializeField] Animator m_Aniamtor;
    [SerializeField] GameObject m_OpenAgainBoxButton;
    [SerializeField] GameObject m_OpenBoxButton;
    [SerializeField] GameObject m_ItemInfo;
    [SerializeField] TMP_Text m_ItemNameText;
    [SerializeField] TMP_Text m_ItemDescribeText;

    GameObject[] m_LotteryObjects;

    int m_CurrentSelectedIndex;

    // Use this for initialization
    void Start()
    {
        m_LotteryObjects = new GameObject[m_Lottery.Length];
        for (int i = 0; i < m_Lottery.Length; i++)
        {
            GameObject obj = Instantiate(m_Lottery[i]._prefab, Vector3.zero, Quaternion.identity, m_LotteryObjectRoot);
            obj.SetActive(false);
            m_LotteryObjects[i] = obj;
        }
    }


    int Choose()
    {
        float total = 0;

        for (int i = 0; i < m_Lottery.Length; i++)
        {

            total += m_Lottery[i]._probability;
        }
        Debug.Log("total:" + total);
        float randomPoint = Random.value * total;
        Debug.Log("randomPoint" + randomPoint);

        for (int i = 0; i < m_Lottery.Length; i++)
        {
            if (randomPoint < m_Lottery[i]._probability)
            {
                Debug.Log("i" + i);
                return i;

            }
            else
            {
                randomPoint -= m_Lottery[i]._probability;
                Debug.Log("randomPoint" + randomPoint);
            }
        }
        Debug.Log("m_Loots.Length - 1" + (m_Lottery.Length - 1));
        return m_Lottery.Length - 1;
    }


    public void OpenBox()
    {
        m_Aniamtor.SetBool("Open", true);
        m_CurrentSelectedIndex = Choose();
        m_LotteryObjects[m_CurrentSelectedIndex].SetActive(true);

    }

    public void ShowOpenAgainButton()
    {
        m_OpenAgainBoxButton.SetActive(true);

        m_ItemNameText.text = m_Lottery[m_CurrentSelectedIndex]._name;
        m_ItemDescribeText.text = m_Lottery[m_CurrentSelectedIndex]._describe;
        m_ItemInfo.SetActive(true);
    }
    public void ShowOpenButton()
    {
        m_OpenBoxButton.SetActive(true);
    }

    public void ResetBox()
    {
        m_Aniamtor.SetBool("Open", false);
        m_LotteryObjects[m_CurrentSelectedIndex].SetActive(false);
        m_ItemInfo.SetActive(false);
    }
}

[System.Serializable]
public class LotteryBoxItem
{
    [Header("物品設定")]
    public int _probability;
    public GameObject _prefab;
    [Header("物品資料")]
    public string _name;
    public string _describe;
}
