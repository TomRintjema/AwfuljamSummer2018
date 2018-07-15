﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour {
    public string goalKey = "default";
    public bool hasMessage = true;
    public string nextMessage = "horse";
    public bool hasGroup = true;
    public GameObject group;
    private bool hasTriggered = false;
    private Text messageDisplayText;
    private GameObject messageDisplay;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<KeyCode>() != null)
        {
            if ((other.gameObject.GetComponent<KeyCode>().key == goalKey) && (!hasTriggered))
            {
                Debug.Log("Goal!");
                hasTriggered = true;
                if (hasMessage)
                {
                    UpdateMessage();
                }
                if (hasGroup)
                {
                    ShowGroup();
                }
            }
        }
    }
    
    void UpdateMessage()
    {
        messageDisplay = GameObject.FindWithTag("MessageHudText");
        messageDisplayText = messageDisplay.GetComponent(typeof(Text)) as Text;
        messageDisplayText.text = nextMessage;
    }

    void ShowGroup()
    {
        group.SetActive(true);
    }
       
}
