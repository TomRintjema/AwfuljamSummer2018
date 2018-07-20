using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class HookScript : MonoBehaviour {
    GameObject ship;
    GameObject heldBox;
    GameObject hookDisplay;
    Text hookDisplayText;
    public bool hooking = true;
    
    private void Start()
    {
        hookDisplay = GameObject.FindWithTag("HookHudText");
        hookDisplayText = hookDisplay.GetComponent(typeof(Text)) as Text;
        hookDisplayText.text = "Armed";
    }

    public void ArmDisarm()
    {
        if (heldBox == null)
        {
            if (!hooking)
            {
                Debug.Log("Hook Armed");
                hooking = true;
                hookDisplayText.text = "Armed";
            } else
            {
                Debug.Log("Hook Disarmed");
                hooking = false;
                hookDisplayText.text = "Disarmed";
                
            }

        }

        if (heldBox != null)
        {
            Detach();
        }
    }

    public void Detach()
    {
        hookDisplayText.text = "Disarmed";
        if (heldBox != null)
        {
            heldBox.transform.parent = null;
            gameObject.GetComponent<FixedJoint2D>().connectedBody = null;
            gameObject.GetComponent<FixedJoint2D>().enabled = false;
            heldBox = null;
            Debug.Log("Hook detached, Ready to arm");
            //hookDisplayText.text = "Disarmed";
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
                hookDisplayText.text = "Latched";
            }
        }
    }
}
