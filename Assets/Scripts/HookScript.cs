using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class HookScript : MonoBehaviour {
    GameObject ship;
    GameObject heldBox;
    public bool hooking = true;

    public void ArmDisarm()
    {
        if (heldBox == null)
        {
            if (!hooking)
            {
                Debug.Log("Hook Armed");
                hooking = true;
            } else
            {
                Debug.Log("Hook Disarmed");
                hooking = false;
            }

        }

        if (heldBox != null)
        {
            heldBox.transform.parent = null;
            gameObject.GetComponent<FixedJoint2D>().connectedBody = null;
            gameObject.GetComponent<FixedJoint2D>().enabled = false;
            heldBox = null;
            Debug.Log("Hook detached, Ready to arm");
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
                Debug.Log("Hook grabbed something!");
            }
        }
    }
}
