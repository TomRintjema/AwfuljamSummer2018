using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float hitStrength;
    public GameObject chainLinkPrefab;
    public GameObject hookPrefab;
    public GameObject hookPosition;
    public int maxLinkLength;
    public Transform explosionPrefab;
    List<GameObject> chainLinkList = new List<GameObject>();
    GameObject heldHook;

    //Hud Hooks
    public Text fuelText;
    public Text velocityText;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        UpdateFuelText(currentFuel);
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

    private void Update()
    {
        if (Input.GetButtonDown("FireGrappler"))
        {
            FireGrappler();
        }

        if (Input.GetButton("LowerGrappler"))
        {
            LowerGrappler();
        }

        if (Input.GetButton("RaiseGrappler"))
        {
            RaiseGrappler();
        }

        float velocityReadout = Vector2.Distance(Vector2.zero, rb.velocity);
        velocityReadout *= 100f;
        velocityReadout = Mathf.Round(velocityReadout);
        //velocityReadout *= 0.1f;
        UpdateVelocityText(velocityReadout);
    }

    void Thrust(int direction)
    {
        /*Vector3 vel = rb.velocity;
        vel += gameObject.transform.up * thrustSpeed * direction;
        rb.velocity = vel;*/
        if (currentFuel > 0)
        {
            currentFuel -= rofThrust;
            UpdateFuelText(currentFuel);
            rb.AddForce(gameObject.transform.up * thrustSpeed * direction * Time.deltaTime);
            engineParticleL.Play();
            engineParticleR.Play();


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
            //Make three chain links, separate them by some units
            //Add each of the chain links to the chain link list
            int numberLinkToCreate = 3;
            for (int i = 0; i < numberLinkToCreate; i++) { 
                GameObject tempChainLink = Instantiate(chainLinkPrefab, hookPosition.transform.position, Quaternion.identity) as GameObject;
                Debug.Log("Made a link");
                tempChainLink.GetComponent<HingeJoint2D>().enabled = true;
                chainLinkList.Add(tempChainLink);
            }
            //Make the chain links enable their hinge joint
            //Hook the chain links to the chain link above them
            GameObject lastLink = gameObject;
            foreach (GameObject chain in chainLinkList)
            {
                chain.transform.parent = lastLink.transform.parent;
                chain.GetComponent<HingeJoint2D>().connectedBody = lastLink.GetComponent<Rigidbody2D>();
                lastLink = chain;
            }


            //Fire the grappler
            //First, Make the hook
            heldHook = (GameObject)Instantiate(hookPrefab, hookPosition.transform.position, Quaternion.identity);

            //Hook the hook to the lowest chain link
            heldHook.GetComponent<HingeJoint2D>().enabled = true;
            heldHook.GetComponent<HingeJoint2D>().connectedBody = chainLinkList[chainLinkList.Count-1].GetComponent<Rigidbody2D>();

            //Move everything to its position??????

        } else
        {
            //Arm/Disarm the grappler, since it's out
            //This is on the hook so I don't have to worry about it here!
            heldHook.GetComponent<HookScript>().ArmDisarm();
        }
    }

    void LowerGrappler()
    {
        //If the hook doesn't exist, you can't lower the grappler
        //If the list of chainlinks are < maxchainlength add a new chainlink, and hook the previous last to the newest
        //if the list of chainlinks are >= maxchainlength, do nothing
        if (heldHook == null)
        {
            //You can't lower the grappler if it's not out, you need to fire it.
        } else if (chainLinkList.Count < maxLinkLength)
        {
            AddLinkToChain();
        }
    }

    void AddLinkToChain()
    {
        //TODO: this
        //Add new link to the chain
        //Make a new link where the highest link is
        //enable hinge, make parent of this link the ship
        //make parent of lower link this link
        GameObject tempChainLink = Instantiate(chainLinkPrefab, hookPosition.transform.position, Quaternion.identity) as GameObject;
        Debug.Log("Made a link");
        tempChainLink.GetComponent<HingeJoint2D>().enabled = true;
        GameObject lastLink = chainLinkList[chainLinkList.Count - 1];
        tempChainLink.transform.parent = lastLink.transform.parent;
        tempChainLink.GetComponent<HingeJoint2D>().connectedBody = lastLink.GetComponent<Rigidbody2D>();
        chainLinkList.Add(tempChainLink);

        heldHook.GetComponent<HingeJoint2D>().connectedBody = chainLinkList[chainLinkList.Count - 1].GetComponent<Rigidbody2D>();

    }

    void RaiseGrappler()
    {
        //If the hook doesn't exist, Raising the hook does nothing
        //If the list of chainlink's are 0, raising the grappler will retract the hook completely
        //if the list of chainlinks are > 0, raising the grappler will remove the 0th, and make the 1st the new 0th more or less
        if (heldHook == null)
        {
            //You can't raise the hook if it doesn't exist.
        } else if (chainLinkList.Count == 0)
        {
            RemoveHook();
        } else if (chainLinkList.Count > 0)
        {
            RemoveLink();
        }
    }

    void RemoveLink()
    {
        //TODO: this
        //Make hook attach to higher link in chain
        //if chain is empty, link directly to ship
        //find lowest link, remove it
    }
    
    void RemoveHook()
    {
        Destroy(heldHook);
        heldHook = null;
    }

    void UpdateFuelText(float number)
    {
        fuelText.text = "" + number;
    }

    void UpdateVelocityText(float number)
    {
        velocityText.text = "" + number;
    }
}
