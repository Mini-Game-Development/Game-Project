using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LotteryBoxSystem : MonoBehaviour
{
    [SerializeField]
    public LotteryItemBase[] m_Loots;
    public Transform m_LootObjectParent;
    public Animator m_Aniamtor;
    public GameObject m_OpenAgainBoxButton;
    public GameObject m_OpenBoxButton;
    public GameObject m_ItemInfo;
    public TMP_Text ItemNameText ,ItemDescribeText;


    public GameObject[] m_LootObjects;
    Vector3  OldLotteryItemPosition;
    public float NewLotteryItemPositionNumber =1f;
    int m_CurrentSelectedIndex;

    // Use this for initialization
    void Start()
    {
        m_LootObjects = new GameObject[m_Loots.Length];
        for (int i = 0; i < m_Loots.Length; i++)
        {
            GameObject obj = Instantiate(m_Loots[i]._prefab, Vector3.zero, Quaternion.identity, m_LootObjectParent);
            obj.SetActive(false);
            m_LootObjects[i] = obj;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    int Choose()
    {
        float total = 0;

        for (int i = 0; i < m_Loots.Length; i++)
        {
            total += m_Loots[i]._probability;
        }
        float randomPoint = Random.value * total;

        for (int i = 0; i < m_Loots.Length; i++)
        {
            if (randomPoint < m_Loots[i]._probability)
            {
                return i;
            }
            else
            {
                randomPoint -= m_Loots[i]._probability;
            }
        }
        return m_Loots.Length - 1;
    }


    public void OpenBox()
    {
        m_Aniamtor.SetBool("Open", true);
        m_CurrentSelectedIndex = Choose();
        int itemId = m_CurrentSelectedIndex + 3;
        PropsItemManager.Instance.AddItem(itemId);
        m_LootObjects[m_CurrentSelectedIndex].SetActive(true);

    }

    public void ShowOpenAgainButton()
    {
        m_OpenAgainBoxButton.SetActive(true);
        ItemNameText.text = m_Loots[m_CurrentSelectedIndex]._name;
        ItemDescribeText.text = m_Loots[m_CurrentSelectedIndex]._describe;
        m_ItemInfo.SetActive(true);

        OldLotteryItemPosition = m_LootObjects[m_CurrentSelectedIndex].transform.position;
        m_LootObjects[m_CurrentSelectedIndex].transform.position += new Vector3(0, NewLotteryItemPositionNumber, 0);
        Floater _floater;
        _floater = m_LootObjects[m_CurrentSelectedIndex].GetComponent<Floater>();
        _floater.startFloater();
        _floater.ItemFloaterState = true;
        
    }
    public void ShowOpenButton()
    {
        m_OpenBoxButton.SetActive(true);
    }

    public void ResetBox()
    {
        m_Aniamtor.SetBool("Open", false);
        
        Floater _floater;
        _floater = m_LootObjects[m_CurrentSelectedIndex].GetComponent<Floater>();
        _floater.ItemFloaterState = false;
        m_LootObjects[m_CurrentSelectedIndex].transform.position = OldLotteryItemPosition;
        m_LootObjects[m_CurrentSelectedIndex].SetActive(false);
        m_ItemInfo.SetActive(false);
    }
}

[System.Serializable]
public class LotteryItemBase
{
    [Header("物品設定")]
    public int _probability;
    public GameObject _prefab;
    [Header("物品資料")]
    public string _name;
    public string _describe;
}
