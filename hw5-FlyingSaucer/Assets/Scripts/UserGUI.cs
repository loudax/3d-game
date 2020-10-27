using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
	private IUserAction action;
	public int life = 6;   
	public bool go = true;

	void Start ()
	{
		action = SSDirector.GetInstance().CurrentSceneControllor as IUserAction;
	}

	void OnGUI ()
	{	
		if (Input.GetButtonDown("Fire1")){
			Vector3 pos = Input.mousePosition;
			action.Hit(pos);
		}

		GUI.Label(new Rect(20, 10, 200, 50), "score:");
		GUI.Label(new Rect(60, 10, 200, 50), action.GetScore().ToString());
		GUI.Label(new Rect(20, 30, 50, 50), "hp:");
		for (int i = 0; i < life; i++){
			GUI.Label(new Rect(40 + 10 * i, 30, 50, 50), "X");
		}
		GUI.Label(new Rect(20, 50, 50, 50), "trail:");
		GUI.Label(new Rect(60, 50, 50, 50), action.GetTrail().ToString());

		if(action.GetRound() == 1){
			GUI.Label(new Rect(200, 10, 200, 50), "ROUND");
			GUI.Label(new Rect(250, 10, 200, 50), action.GetRound().ToString());
			GUI.Label(new Rect(200, 30, 300, 50), "分数到达5分进入下一关，打中一个飞碟得1分，错过一个飞碟生命值减1，生命值归零时游戏失败。");
		}
		else if(action.GetRound() == 2){
			GUI.Label(new Rect(200, 10, 200, 50), "ROUND");
			GUI.Label(new Rect(250, 10, 200, 50), action.GetRound().ToString());
			GUI.Label(new Rect(200, 30, 300, 50), "分数到达15分进入下一关，打中一个飞碟得2分，错过一个飞碟生命值减1，生命值归零时游戏失败。");
		}
		else if(action.GetRound() == 3){
			GUI.Label(new Rect(200, 10, 200, 50), "ROUND");
			GUI.Label(new Rect(250, 10, 200, 50), action.GetRound().ToString());
			GUI.Label(new Rect(200, 30, 300, 50), "分数到达30分获得胜利，打中一个飞碟得3分，错过一个飞碟生命值减1，生命值归零时游戏失败。");
		}
		else{
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 300, 100, 100), "You Win!");
		}

		if(GUI.Button(new Rect(550, 10, 100, 40), "Pause")){
			go = false;
		}
		if(go == false){
			if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.width / 2 - 250, 100, 40), "Continue")){
				go = true;
			}
		}

		if (GUI.Button(new Rect(670, 10, 100, 40), "Restart")){
			action.Restart();
			return;
		}

		if (life == 0){
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 300, 100, 100), "Game Over!");
			if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.width / 2 - 250, 100, 40), "Restart")){
				action.Restart();
				return;
			}
		}	
	}
    
	public void ReduceBlood() //生命值减1
	{
		if(life > 0)
			life--;
	}

	public void RecoverBlood() //生命值恢复初始状态
	{
		life = 6;
	}
}