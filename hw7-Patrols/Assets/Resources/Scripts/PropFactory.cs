using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFactory : MonoBehaviour
{
    private GameObject patrol = null;  
    private List<GameObject> used = new List<GameObject>();               
    private Vector3[] pos = new Vector3[9]; 
    public Controller sceneController;

    public List<GameObject> GetPatrols()
    {
        int[] pos_x = { -6, 4, 13 };
        int[] pos_z = { -4, 6, -13 };
        int[] num = {8, 1, 9, 7, 2, 6, 4, 3, 5};
        int index = 0;
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                pos[index] = new Vector3(pos_x[i], 5, pos_z[j]);
                index++;
            }
        }
        for(int i = 0; i < 9; i++){
            patrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrol"));
            patrol.transform.position = pos[i];
            patrol.GetComponent<PatrolData>().sign = num[i];
            patrol.GetComponent<PatrolData>().start_position = pos[i];
            used.Add(patrol);
        }   
        return used;
    }
}
