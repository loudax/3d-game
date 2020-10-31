using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
	private IUserAction action;
	GUIStyle style1 = new GUIStyle();
	GUIStyle style2 = new GUIStyle();
	private bool game_start = false;       

	void Start ()
	{
		action = SSDirector.GetInstance().CurrentSceneControllor as IUserAction;
		style1.fontSize = 15;
		style2.fontSize = 25;
	}

	void Update()
	{
		if(game_start)
		{
			if (action.haveArrowOnPort ()) {
				if (Input.GetMouseButton(0)) {
					Vector3 mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
					action.MoveBow(mousePos);
				}
				if (Input.GetMouseButtonUp(0)) {
					Vector3 mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
					action.Shoot(mousePos);
				}
			}
			if (Input.GetKeyDown(KeyCode.Space)) action.create();
		}
	}

	private void OnGUI()
	{
		if(game_start){
				GUI.Label(new Rect(10, 10, 200, 50), "分数:", style1);
				GUI.Label(new Rect(55, 10, 200, 50), action.GetScore().ToString(), style1);
				GUI.Label(new Rect(Screen.width - 170, 30, 200, 50), "风向:", style1);
				GUI.Label(new Rect(Screen.width - 140, 30, 200, 50), action.GetWind(), style1);
			if (GUI.Button(new Rect(Screen.width - 170, 0, 100, 30), "重新开始")){
					action.Restart();
					return;
				}
		}
		else{
			GUI.Label(new Rect(Screen.width / 2 - 80, Screen.width / 2 - 320, 100, 100), "Arrow Shooting", style2);
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 200, 100, 50), "游戏开始")){
				game_start = true;
				action.BeginGame();
			}
		}
	}
}