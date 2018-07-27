using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public float maxX = 100f;
    public float minX = -100f;

    GameObject playerShip;
    
    //private Rigidbody2D playerRididbody2D;

	// Use this for initialization
	void Start () {
        playerShip = GameObject.FindWithTag("Player");
        //playerRididbody2D = playerShip.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerShip != null)
        {
            Vector3 posNew = new Vector3(playerShip.transform.position.x, playerShip.transform.position.y, -10);
            if (posNew.x > maxX) { posNew.x = maxX; }
            if (posNew.x < minX) { posNew.x = minX; }
            gameObject.transform.position = posNew;
            //gameObject.transform.position = new Vector3(playerShip.transform.position.x + playerRididbody2D.velocity.x, playerShip.transform.position.y + playerRididbody2D.velocity.y, -10);
        }
    }
}
