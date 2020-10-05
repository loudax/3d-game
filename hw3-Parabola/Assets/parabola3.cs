using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parabola3 : MonoBehaviour
{
    public float speed = 1;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = new Vector3(Time.deltaTime * 5, - Time.deltaTime * (speed / 10), 0);
        this.transform.Translate(v);    
        speed++;
    }
}
