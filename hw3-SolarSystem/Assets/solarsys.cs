using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solarsys : MonoBehaviour
{
    public Transform sun;
    public Transform mercury;
    public Transform venus;
    public Transform earth;
    public Transform mars;
    public Transform jupiter;
    public Transform saturn;
    public Transform uranus;
    public Transform neptune;
    public Transform moon;
    Vector3 []a = new Vector3[8];
    float []v = new float[8];
    float speed = 40;
    // Start is called before the first frame update
    void Start()
    {
        int i;
        for(i = 0; i < 8; i++){
            a[i] = new Vector3(0, Random.Range(1, 360), Random.Range(1, 360));
            v[i] = speed + Random.Range(0, 30);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mercury.RotateAround(sun.position, a[0], v[0] * Time.deltaTime);
        mercury.Rotate(Vector3.up * speed * Time.deltaTime);
        venus.RotateAround(sun.position, a[1], v[1] * Time.deltaTime);
        venus.Rotate(Vector3.up * speed * Time.deltaTime);
        earth.RotateAround(sun.position, a[2], v[2] * Time.deltaTime);
        earth.Rotate(Vector3.up * speed * Time.deltaTime);
        moon.RotateAround(earth.position, Vector3.up, 359 * Time.deltaTime);    
        mars.RotateAround(sun.position, a[3], v[3] * Time.deltaTime);
        mars.Rotate(Vector3.up * speed * Time.deltaTime);
        jupiter.RotateAround(sun.position, a[4], v[4] * Time.deltaTime);
        jupiter.Rotate(Vector3.up * speed * Time.deltaTime);
        saturn.RotateAround(sun.position, a[5], v[5] * Time.deltaTime);
        saturn.Rotate(Vector3.up * speed * Time.deltaTime);
        uranus.RotateAround(sun.position, a[6], v[6] * Time.deltaTime);
        uranus.Rotate(Vector3.up * speed * Time.deltaTime);
        neptune.RotateAround(sun.position, a[7], v[7] * Time.deltaTime);
        neptune.Rotate(Vector3.up * speed * Time.deltaTime);  
    }
}
