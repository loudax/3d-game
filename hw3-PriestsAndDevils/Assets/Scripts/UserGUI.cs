using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game;

public class UserGUI : MonoBehaviour {

    private IUserAction action;
    public int sign = 0;
    public float time = 60;

    void Start() {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }

    void Update(){
        
    }

    void OnGUI() {
        time -= Time.deltaTime/2;
        if(sign == 0){
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 4 + 30, 60, 30), time.ToString());
            if(time < 0){
                sign = 1;
            }
        }
        if(GUI.Button(new Rect(Screen.width / 2 - 30, Screen.height / 4, 60, 20), "Restart")) {
            action.Restart();
            sign = 0;
            time = 60;
        }
        if(sign == 1){
            GUI.Label(new Rect(Screen.width / 2 - 35, Screen.height / 4 + 30, 70, 30), "Gameover!");
        }
        if(sign == 2){
            GUI.Label(new Rect(Screen.width / 2 - 35, Screen.height / 4 + 30, 70, 30), "You Win!");
        }
    }
}