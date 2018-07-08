using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour {
    GameObject ship;
    public bool hooking = true;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hooking)
        {
            if (collision.gameObject.tag == "Box")
            {
                GameObject box = collision.gameObject;
                box.transform.parent = gameObject.transform;
                gameObject.GetComponent<DistanceJoint2D>().connectedBody = box.GetComponent<Rigidbody2D>();
                gameObject.GetComponent<DistanceJoint2D>().enabled = true;
            }
        }
    }
}
