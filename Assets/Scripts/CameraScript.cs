using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    GameObject playerShip;

	// Use this for initialization
	void Start () {
        playerShip = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3 (playerShip.transform.position.x, playerShip.transform.position.y, -10);
	}
}
