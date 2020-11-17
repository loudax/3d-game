using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private IUserAction action;
    private GUIStyle style = new GUIStyle();

    void Start ()
    {
        style.fontSize = 25;
        action = SSDirector.GetInstance().CurrentSceneController as IUserAction;
    }

    void Update()
    {
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        action.MovePlayer(translationX, translationZ);
    }

    private void OnGUI()
    {
		GUI.Label(new Rect(10, 5, 200, 50), "Score:");
		GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString());
        if(action.GetGameover()){
            GUI.Label(new Rect(Screen.width / 2 - 70, 80, 200, 50), "Game Over", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, 150, 100, 50), "Restart"))
            {
                action.Restart();
                return;
            }
        }
        if(action.GetWin()){
            GUI.Label(new Rect(Screen.width / 2 - 70, 80, 200, 50), "You Win", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, 150, 100, 50), "Restart"))
            {
                action.Restart();
                return;
            }
        }
    }
}
