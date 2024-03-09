using UnityEngine;
using Game;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PropsButtonSetting : MonoBehaviour
{
    public string ItemGameObjectText;
    public TMP_Text CurrentCountText;
    public int CurrentCount;
    [SerializeField]
    private PropsSystem UIPropsListStateManager;
    public string Describe;
    public string ChangeImage;
    public string ColorString;
    // Start is called before the first frame update

    void Start()
    {
        TMP_Text SortText;
        SortText = CurrentCountText.GetComponent<TMP_Text>();
        SortText.text = CurrentCount.ToString();
    }

    public void getCurrentSortButtonActive()
    {
        UIPropsListStateManager.UpdateDisplay(ItemGameObjectText,ChangeImage, ColorString, Describe);
        PropsDataManager.Instance.ChooseItemName = ItemGameObjectText.ToString();
    }
}

