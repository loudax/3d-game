using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IMGUI : MonoBehaviour {

	public float health;
    public Scrollbar bar2;

	void Start(){
		health = 1.0f;
	}

	void OnGUI(){
		if (GUI.Button (new Rect(160, 70, 40, 20), "+")) {
            if (health + 0.1f > 1.0f){
                health = 1.0f;
            }
            else{
                health += 0.1f;
            }
		}
		if (GUI.Button (new Rect(80, 70, 40, 20), "-")) {
            if (health - 0.1f < 0.0f){
                health = 0.0f;
            }
            else{
                health -= 0.1f;
            }
		}
		GUI.color = Color.red;
		GUI.HorizontalScrollbar(new Rect(40, 40, 200, 20), 0.0f, health, 0.0f, 1.0f);
        bar2.value = health;
	}

}

