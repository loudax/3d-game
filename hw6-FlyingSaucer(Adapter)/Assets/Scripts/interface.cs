using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneControllor
{
	void LoadResources ();
}

public interface IUserAction
{
	void Hit(Vector3 pos);
	void Restart();
	int GetScore();
	void setting(float speed);
	int GetRound();
	int GetTrail();
	bool GetMode();
	void SetMode(bool flag);
	bool GetModeSetting();
	void StartGame();
}

public interface IActionManager
{
	void UFOFly(GameObject disk, float angle, float power, bool go, bool flag); // flag == true 物理学， flag == false 运动学 
}