using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController{void LoadResources();}
public interface IUserAction                          
{
    void MovePlayer(float translationX, float translationZ);
    int GetScore();
    bool GetGameover();
    bool GetWin();
    void Restart();
}
public interface ISSActionCallback{void SSActionEvent(SSAction source,int intParam = 0,GameObject objectParam = null);}
public interface IGameStatusOp
{
    void PlayerEscape();
    void PlayerGameover();
}
