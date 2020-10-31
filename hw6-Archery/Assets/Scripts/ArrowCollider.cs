using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour
{
	public Controllor scene_controller;
	public ScoreRecorder recorder;                   

	void Start()
	{
		scene_controller = SSDirector.GetInstance().CurrentSceneControllor as Controllor;
		recorder = Singleton<ScoreRecorder>.Instance;
	}

	void OnTriggerEnter(Collider c)
	{ 
		if (c.gameObject.name == "Cylinder1"||c.gameObject.name == "Cylinder2"||c.gameObject.name == "Cylinder3"||c.gameObject.name == "Cylinder4"||c.gameObject.name == "Cylinder5") {
			gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			gameObject.SetActive(false);
			float point = Mathf.Sqrt (this.gameObject.transform.position.x * this.gameObject.transform.position.x + this.gameObject.transform.position.y * this.gameObject.transform.position.y);
			recorder.Record(5-(int)Mathf.Floor(point*2));
		}

	}
}