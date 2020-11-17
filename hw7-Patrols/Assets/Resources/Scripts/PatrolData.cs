using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolData : MonoBehaviour
{
    public int sign;
    public bool follow_player = false;
    public int wall_sign = -1;
    public GameObject player;
    public Vector3 start_position;
}
