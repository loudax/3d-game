using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : MonoBehaviour
{
    public int sign = 0;
    public Controller sceneController;

    private void Start()
    {
        sceneController = (Controller)SSDirector.GetInstance().CurrentSceneController;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player(Clone)"){
            sceneController = (Controller)SSDirector.GetInstance().CurrentSceneController;
            sceneController.wall_sign = sign;
        }     
    }
}
