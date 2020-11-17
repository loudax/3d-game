using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public Controller sceneController;
    public int score = 0; 

    void Start() 
    {
        sceneController = (Controller)SSDirector.GetInstance().CurrentSceneController;
        sceneController.recorder = this;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore()
    {
        score++;
    }
}