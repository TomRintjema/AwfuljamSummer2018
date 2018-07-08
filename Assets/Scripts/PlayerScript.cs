using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    Rigidbody2D rb;
    public float thrustSpeed = .05f;
    public float rotSpeed = 1f;
    public float maxFuel;
    public float currentFuel;
    public float rofThrust; //Rate of fuel loss on thrusting
    public float rofRot; //Rate of fuel loss on rotating
    public ParticleSystem engineParticleR;
    public ParticleSystem engineParticleL;

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
        }
    }
}
