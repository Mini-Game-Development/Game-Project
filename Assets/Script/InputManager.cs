using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            GameEventManager.Instance.SendEvent(GameEvent.Input_W);
        if (Input.GetKeyDown(KeyCode.A))
            GameEventManager.Instance.SendEvent(GameEvent.Input_A);
        if (Input.GetKeyDown(KeyCode.S))
            GameEventManager.Instance.SendEvent(GameEvent.Input_S);
        if (Input.GetKeyDown(KeyCode.D))
            GameEventManager.Instance.SendEvent(GameEvent.Input_D);


        if(Input.GetMouseButtonDown(0))
            GameEventManager.Instance.SendEvent(GameEvent.Input_MouseRightDown);
        if (Input.GetMouseButtonUp(0))
            GameEventManager.Instance.SendEvent(GameEvent.Input_MouseRightUp);
        if (Input.GetMouseButtonUp(0))
            GameEventManager.Instance.SendEvent(GameEvent.Input_MouseLeftDown);
        if (Input.GetMouseButtonUp(0))
            GameEventManager.Instance.SendEvent(GameEvent.Input_MouseLeftUp);
    }
}
