using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : MonoBehaviour
{
    static GameEventManager instance;
    public static GameEventManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindFirstObjectByType<GameEventManager>();

            if (instance != null)
                return instance;

            return null;
        }
    }
    private Dictionary<int, UnityAction<string>> eventDic = new Dictionary<int, UnityAction<string>>();

    public void RegisterEvent(GameEvent gameEvent, UnityAction<string> action)
    {
        if (gameEvent == GameEvent.None)
            return;

        int eventInt = (int)gameEvent;
        if (eventDic.TryGetValue(eventInt, out UnityAction<string> eventAction))
        {
            eventAction += action;
            eventDic[eventInt] = eventAction;
        }
        else
        {
            eventDic.Add(eventInt, action);
        }
    }

    public void SendEvent(GameEvent gameEvent, string param = "")
    {
        if (gameEvent == GameEvent.None)
            return;

        ActiveEvent(gameEvent, param);
    }
    public void ActiveEvent(GameEvent gameEvent, string param = "")
    {
        int gameEventInt = (int)gameEvent;

        if (eventDic.TryGetValue(gameEventInt, out var action))
        {
            if (action != null)
            {
                action(param);
            }
        }
    }
}
