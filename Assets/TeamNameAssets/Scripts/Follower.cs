using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
    public Transform target;
    public bool x;
    public bool y;
    public bool z;
    private Vector3 targetPos;

	// Use this for initialization
	void Start () {
        targetPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (x)
        {
            targetPos.x = target.position.x;
        }
        if(y)
        {
            targetPos.y = target.position.y;
        }
        if(z)
        {
            targetPos.z = target.position.z;
        }
        transform.position = targetPos;
    }
}
