using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
    Rigidbody2D rb;
    public bool playerHasControl = false;
    public float thrustSpeed;
    public float rotSpeed;
    public float maxFuel;
    public float currentFuel;
    public float rofThrust; //Rate of fuel loss on thrusting
    public float rofRot; //Rate of fuel loss on rotating
    public ParticleSystem engineParticleR;
    public ParticleSystem engineParticleL;
    public float hitStrength;
    public GameObject hookPrefab;
    public Transform explosionPrefab;
    public GameObject hookPosition;
    public float deadzone;
    public GameObject masterHook;
    public GameObject heldHook;
    private AudioSource engineSound;
    
    //Hud Hooks
    public Text fuelText;
    public Text velocityText;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        UpdateFuelText(currentFuel);
        engineSound = GetComponent<AudioSource> ();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        engineParticleL.Stop();
        engineParticleR.Stop();
        if (playerHasControl)
        {
            if (Input.GetButton("Thrust"))
            {
                Thrust(1);
                //} else if (Input.GetKey(KeyCode.S)) {
                //    Thrust(-1);
            }


            if (Input.GetButton("RotateLeft"))
            {
                Rotate(1);
            }
            else if (Input.GetButton("RotateRight"))
            {
                Rotate(-1);
            }
            
            float horiz = Input.GetAxis("Horizontal");
            float verti = Input.GetAxis("Vertical");
            horiz *= -1f;
            verti *= -1f;

            if (verti > deadzone)
            {
                Thrust(verti);
            }

            if ((horiz < -deadzone)  || (horiz > deadzone))
            {
                Rotate(horiz);
            }
        }
	}

    private void Update()
    {
        if (Input.GetButtonDown("FireGrappler"))
        {
            FireGrappler();
        }

        float velocityReadout = Vector2.Distance(Vector2.zero, rb.velocity);
        velocityReadout *= 100f;
        velocityReadout = Mathf.Round(velocityReadout);
        //velocityReadout *= 0.1f;
        UpdateVelocityText(velocityReadout);
    }

    void Thrust(float direction)
    {
        /*Vector3 vel = rb.velocity;
        vel += gameObject.transform.up * thrustSpeed * direction;
        rb.velocity = vel;*/
        if (currentFuel > 0)
        {
            currentFuel -= rofThrust * direction;
            UpdateFuelText(currentFuel);
            rb.AddForce(gameObject.transform.up * thrustSpeed * direction * Time.deltaTime);
            //Debug.Log("Thrust Force " + thrustSpeed * direction * Time.deltaTime);
            engineParticleL.Play();
            engineParticleR.Play();
            if (engineSound.isPlaying == false)
            {
                engineSound.Play();
            }
            


            if (currentFuel < 0)
            {
                currentFuel = 0;
                UpdateFuelText(currentFuel);
            }
        } else
        {
            //Tom put your L & R "No fuel" smokey thing here
        }
    }

    void Rotate(float direction)
    {
        //transform.Rotate(Vector3.forward * Time.deltaTime * direction * rotSpeed);
        if (currentFuel > 0)
        {
            if (engineSound.isPlaying == false)
            {
                engineSound.Play();
            }

            currentFuel -= rofRot * Mathf.Abs(direction);
            UpdateFuelText(currentFuel);
            rb.AddTorque(direction * rotSpeed * Time.deltaTime);
            //Debug.Log("Rot force " + direction * rotSpeed * Time.deltaTime);
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
            Debug.Log(collision.collider.tag);
            if (collision.collider.tag != "Safe")
            {
                if (collision.collider.tag != "Hook")
                {
                    Kaboom();
                }
            }
        }
    }

    void Kaboom()
    {
        Debug.Log("YOU DEAD NOW!");
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);

    }

   
    /*
     * OK So some words so I can remember this 'cause otherwise I'll forget.
     * The prefab of the hook and the chain link is to just grab that from the prefab list
     * The chainlinklist is so I can make sure that the list can grow and shrink easilly
     * the heldhook is if it's out, if it's not out then that's "there is no hook out"
     */

    void FireGrappler()
    {
        //if the hook is out already, make the hook armed, or disarmed if armed
        //If the hook is not out, make it fire out and give it like 3 chain links
        if (heldHook == null)
        {
            //Make Hook
            masterHook = Instantiate(hookPrefab, hookPosition.transform.position, Quaternion.identity) as GameObject;
            heldHook = GameObject.FindWithTag("Hook");
            HingeJoint2D hj = masterHook.GetComponent<HingeJoint2D>();
            hj.enabled = true;
            hj.connectedBody = gameObject.GetComponent<Rigidbody2D>();
        }
        else
        {
            //Destroy the hook
            heldHook.GetComponent<HookScript>().Detach();


            GameObject temp = masterHook;
            masterHook = null;
            heldHook = null;
            Destroy(temp);

            }
    }

    void UpdateFuelText(float number)
    {
        fuelText.text = "" + number;
    }

    void UpdateVelocityText(float number)
    {
        velocityText.text = "" + number;
    }

    public void GivePlayerControl()
    {
        playerHasControl = true;
    }

    public void GiveFuel(float fuelToGive)
    {
        currentFuel = fuelToGive;
        UpdateFuelText(currentFuel);
    }
}
