using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Awake () {
        Debug.Log("Awake");
    }
 
    void Start () {
        Debug.Log("Start");
	}
	
    void Update () {
        Debug.Log("Update");
	}
 
    void FixedUpdate () {
        Debug.Log("FixedUpdate");
    }

    void LateUpdate () {
        Debug.Log("LateUpdate");
    }
 
    void OnGUI () {
        Debug.Log("OnGUI");
    }
 
    void OnDisable () {
        Debug.Log("OnDisable");
    }
 
    void OnEnable () {
        Debug.Log("OnEnable");
    }
}
