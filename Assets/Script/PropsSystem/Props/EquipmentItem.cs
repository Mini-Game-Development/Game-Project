using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : PropsItem
{
    public override bool TrySetData(string dataString)
    {
        var props = JsonUtility.FromJson<Props>(dataString);
        if (props == null)
        {
            propsId = 0;
            currentCount = 0;
            RefreshUI();
            return false;
        }
        else if (PropsDataManager.Instance.TryGetItemOfID(props.propsId, out PropsData targetData))
        {
            if (targetData.isEquiptable)
            {
                propsId = props.propsId;
                currentCount = props.CurrentCount;
                RefreshUI();
            }
            return targetData.isEquiptable;
        }
        else
        {
            propsId = 0;
            currentCount = 0;
            RefreshUI();
            return false;
        }
    }

    public override void Action()
    {
        //base.Action();
    }
}
