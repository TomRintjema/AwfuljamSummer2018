using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundStretch : MonoBehaviour {
    public float tileHeight = 2f;

    // Use this for initialization
    void Start() {
        Vector3 newScale = new Vector3(1, transform.position.y * 0.5f, 1);
        transform.localScale = newScale;
        Vector3 newPosition = new Vector3(transform.position.x, (transform.position.y * 0.5f) - tileHeight, transform.position.z);
        transform.position = newPosition;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
