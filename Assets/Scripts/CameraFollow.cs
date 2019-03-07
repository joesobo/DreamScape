using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float dist = -100;
    private Vector3 vectorSet;

    private void Update()
    {
        if(target != null)
        {
            vectorSet.z = dist;
            vectorSet.x = target.position.x;
            vectorSet.y = target.position.y;
            transform.position = vectorSet;
        }
    }
}
