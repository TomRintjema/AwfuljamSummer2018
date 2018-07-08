using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCheckerScript : MonoBehaviour {
    public float threshhold;
    void OnTriggerStay2D (Collider2D other)
    {
        if (other.gameObject.tag == "Box") {
            if (other.gameObject.GetComponent<ContainerScript>().locked == false)
            {

                if (!(other.gameObject.GetComponent<Rigidbody2D>().velocity.y > threshhold) &&
                    !(other.gameObject.GetComponent<Rigidbody2D>().velocity.y < -threshhold))
                {
                    Debug.Log("BOX IS STOPPED AND IN THE HOLE VELOCITY IS " + other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                    other.gameObject.GetComponent<ContainerScript>().locked = true;
                }
            }
        }
    }
}
