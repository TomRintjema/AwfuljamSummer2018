using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    Rigidbody2D rb;
    public float thrustSpeed;
    public float rotSpeed;
    public float maxFuel;
    public float currentFuel;
    public float rofThrust; //Rate of fuel loss on thrusting
    public float rofRot; //Rate of fuel loss on rotating
    public ParticleSystem engineParticleR;
    public ParticleSystem engineParticleL;
    bool grappleOut = false; //If the grapple is out or not.
    public GameObject clawPrefab;
    public float hitStrength;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        engineParticleL.Stop();
        engineParticleR.Stop();
        if (Input.GetButton("Thrust"))
        {
            Thrust(1);
        //} else if (Input.GetKey(KeyCode.S)) {
        //    Thrust(-1);
        }


		if (Input.GetButton("RotateLeft"))
        {
            Rotate(1);
        } else if (Input.GetButton("RotateRight"))
        {
            Rotate(-1);
        }

        if (Input.GetButtonDown("FireGrappler"))
        {
            grappleOut = !grappleOut;
            if (grappleOut == true)
            {
                Debug.Log("Fired Grappler");
            } else
            {
                Debug.Log("Returned Grappler");
            }
        }

        if (Input.GetButton("LowerGrappler"))
        {
            if (grappleOut)
            {
                Debug.Log("Grappler Down");
            }
        }

        if (Input.GetButton("RaiseGrappler"))
        {
            if (grappleOut)
            {
                Debug.Log("Grappler Up");
            }
        }
	}

    void Thrust(int direction)
    {
        /*Vector3 vel = rb.velocity;
        vel += gameObject.transform.up * thrustSpeed * direction;
        rb.velocity = vel;*/
        if (currentFuel > 0)
        {
            currentFuel -= rofThrust;
            rb.AddForce(gameObject.transform.up * thrustSpeed * direction * Time.deltaTime);
            engineParticleL.Play();
            engineParticleR.Play();


            if (currentFuel < 0)
            {
                currentFuel = 0;
            }
        } else
        {
            //Tom put your L & R "No fuel" smokey thing here
        }
    }

    void Rotate(int direction)
    {
        //transform.Rotate(Vector3.forward * Time.deltaTime * direction * rotSpeed);
        if (currentFuel > 0)
        {
            currentFuel -= rofRot;
            rb.AddTorque(direction * rotSpeed * Time.deltaTime);
            if (direction > 0)
            {
                engineParticleR.Play();
            }
            else if (direction < 0)
            {
                engineParticleL.Play();
            }

            if (currentFuel < 0)
            {
                currentFuel = 0;
            }
        } else
        {
            if (direction > 0)
            {
                //Tom put your R "No fuel" smokey thing here
            }
            else if (direction < 0)
            {
                //Tom put your L "No fuel" smokey thing here
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > hitStrength)
        {
            Kaboom();
        }
    }

    void Kaboom()
    {
        Debug.Log("YOU DEAD NOW!");
    }
}
