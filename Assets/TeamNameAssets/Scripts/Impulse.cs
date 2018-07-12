using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : MonoBehaviour {
    private Rigidbody2D rb;
    public float magnitude = 10f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * magnitude);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
