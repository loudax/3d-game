using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundControllor : MonoBehaviour 
{
	private IUserAction action;
	private float speed;

	void Start()
    {
		action = SSDirector.GetInstance().CurrentSceneControllor as IUserAction;
		speed = 2.0f;
	}

	public void loadRoundData(int round)
	{
		switch (round){
            case 1:    
                break;
            case 2:    
                speed = 1.5f;
                action.setting (speed);
                break;
            case 3:
                speed = 1.0f;
                action.setting (speed);
                break;
        }
	}
}
