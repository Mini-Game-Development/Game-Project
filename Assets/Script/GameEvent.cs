using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent
{
    None = 0,
    // KeyBoardInput
    Input_W = 1,
    Input_A = 2, 
    Input_S = 3,
    Input_D = 4,

    //MouseInput
    Input_MouseRightDown = 20,
    Input_MouseRightUp = 21,
    Input_MouseLeftDown = 22,
    Input_MouseLeftUp = 23,


    //Money
    Add_Money = 100,
    Minus_Money = 101,
}
