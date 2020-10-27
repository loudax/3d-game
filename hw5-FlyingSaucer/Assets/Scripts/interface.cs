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
}