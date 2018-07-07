using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    GameObject playerShip;
    //private Rigidbody2D playerRididbody2D;

	// Use this for initialization
	void Start () {
        playerShip = GameObject.FindWithTag("Player");
        //playerRididbody2D = playerShip.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position = new Vector3(playerShip.transform.position.x, playerShip.transform.position.y, -10);
        //gameObject.transform.position = new Vector3(playerShip.transform.position.x + playerRididbody2D.velocity.x, playerShip.transform.position.y + playerRididbody2D.velocity.y, -10);
    }
}
