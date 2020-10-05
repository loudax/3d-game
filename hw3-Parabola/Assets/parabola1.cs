using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabola1 : MonoBehaviour
{
    public float speed = 1; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime * 5;
        this.transform.position += Vector3.down * Time.deltaTime * (speed / 10);
        speed++;         
    }
}
