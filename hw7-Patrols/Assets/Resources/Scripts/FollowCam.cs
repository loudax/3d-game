using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform playerTransform; 
    public Vector3 deviation; 
 
    void Start()
    {
        playerTransform = GameObject.Find("Player(Clone)").GetComponent<Transform>();
        deviation = transform.position - playerTransform.position; 
    }
    void Update()
    {
        transform.position = playerTransform.position + deviation; 
        transform.LookAt (playerTransform);
    }
}