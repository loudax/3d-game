using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public delegate void ScoreEvent();
    public static event ScoreEvent ScoreChange;
    public delegate void GameoverEvent();
    public static event GameoverEvent GameoverChange;
    public delegate void WinEvent();
    public static event WinEvent WinChange;

    public void PlayerEscape()
    {
        if (ScoreChange != null){
            ScoreChange();
        }
    }

    public void PlayerGameover()
    {
        if (GameoverChange != null){
            GameoverChange();
        }
    }

    public void PlayerWin()
    {
        if (WinChange != null){
            WinChange();
        }
    }
}