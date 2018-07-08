using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HookScript : MonoBehaviour {
    GameObject ship;
    GameObject heldBox;
    public bool hooking = true;

    public void ReleaseHook()
    {
        if (heldBox != null)
        {
            hooking = true;
            heldBox.transform.parent = null;
            gameObject.GetComponent<FixedJoint2D>().connectedBody = null;
            gameObject.GetComponent<FixedJoint2D>().enabled = false;
            heldBox = null;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hooking)
        {
            if (collision.gameObject.tag == "Box")
            {
                hooking = false;
                heldBox = collision.gameObject;
                heldBox.transform.parent = gameObject.transform;
                gameObject.GetComponent<FixedJoint2D>().connectedBody = heldBox.GetComponent<Rigidbody2D>();
                gameObject.GetComponent<FixedJoint2D>().enabled = true;
            }
        }
    }
}
