using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    Rigidbody2D rb;
    public float thrustSpeed = .05f;
    public float rotSpeed = 1f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            Thrust(1);
        } else if (Input.GetKey(KeyCode.S)) {
            Thrust(-1);
        }


		if (Input.GetKey(KeyCode.A))
        {
            Rotate(1);
        } else if (Input.GetKey(KeyCode.D))
        {
            Rotate(-1);
        }
	}

    void Thrust(int direction)
    {
        /*Vector3 vel = rb.velocity;
        vel += gameObject.transform.up * thrustSpeed * direction;
        rb.velocity = vel;*/

        rb.AddForce(gameObject.transform.up * thrustSpeed * direction * Time.deltaTime);
    }

    void Rotate(int direction)
    {
        //transform.Rotate(Vector3.forward * Time.deltaTime * direction * rotSpeed);

        rb.AddTorque(direction * rotSpeed * Time.deltaTime);
    }
}
