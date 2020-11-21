using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    private ParticleSystem particleSys;
    private bool colorA;
    private int time;

    void Start()
    {
        particleSys = this.GetComponent<ParticleSystem>();
        colorA = true;
        time = 0;
    }

    void Update()
    {
        changecolor();
    }

    void OnGUI()
	{
        if(Input.GetMouseButtonDown(0)){
			time++;
            if(time%2 == 0){
                colorA = !colorA;
            }
		}
	}

    void changecolor()
    {
        var main = particleSys.main;
        if(colorA){
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(1.0f, 0.6f, 0.6f));
        }
        else{
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.4f, 0.4f, 0.6f));
        }
    }
}
