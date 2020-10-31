using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour 
{
	public int score = 0;

	void Start()
    {
        score = 0;
    }
	
	public void Record(int point)
    {
		score = score + point;
	}
}