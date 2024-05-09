using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsManager : MonoSingleton<PropsManager>
{
    protected override bool _dontDestroyOnLoad => false;
    [SerializeField] private Transform propsItemsRoot;
    [SerializeField] private Transform propsItemPrefab;
    [SerializeField] private List<Props> propsList = new List<Props>();
    private List<PropsItem> propsItemList = new List<PropsItem>();


    const int MaxStacksCount = 30;
    private void Start()
    {
        for(int i = 0; i < MaxStacksCount; ++i)
        {
            Transform temp = Instantiate(propsItemPrefab, propsItemsRoot);
            var item = temp.GetComponent<PropsItem>();
            propsItemList.Add(item);
            Props tempProps = new Props()
            {
                propsId = 0,
                CurrentCount = 0,
            };
            propsList.Add(tempProps);
        }
    }


    private void AddProps(int propsId, int propsCount)
    {
        var props = propsList.Find(target => target.propsId == propsId);
        if (!IsStacksEnough(propsId, propsCount))
            return;

        if(props != null)
        {
            props.CurrentCount += propsCount;
        }
        else
        {
            Props newProp = new Props()
            {
                propsId = propsId,
                CurrentCount = propsCount,
            };
            propsList.Add(newProp);
        }
    }

    private void ReduceProps(int propsId, int propsCount)
    {
        var props = propsList.Find(target => target.propsId == propsId);
        if (!IsPropsEnough(propsId, propsCount))
            return;

        props.CurrentCount -= propsCount;
    }

    private bool IsStacksEnough(int propsId, int propsCount)
    {
        int stackRemain = MaxStacksCount;
        foreach(var props in propsList) 
        {
            var data = PropsDataManager.Instance.GetItemOfID(props.propsId);

            if(data.propsId == propsId)
            {
                stackRemain -= (props.CurrentCount += propsCount) / data.maxStackLimit;
            }
            else
                stackRemain -= props.CurrentCount / data.maxStackLimit;
        }

        return stackRemain >= 0;
    }

    private bool IsPropsEnough(int propsId, int propsCount)
    {
        var propsRecord = propsList.Find(target => target.propsId == propsId);

        if(propsRecord != null)
        {
            return propsRecord.CurrentCount >= propsCount;
        }

        return false;
    }
}
