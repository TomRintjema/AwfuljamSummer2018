﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomInSize = 5f;
    public float zoomOutSize = 10f;
    public float checkDistance = 1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");
        hit = Physics2D.Raycast(this.transform.position, -Vector2.up, checkDistance, layerMask);
        if (hit.collider != null)
        {
            mainCamera.orthographicSize = zoomInSize;
        }
        else
        {
            mainCamera.orthographicSize = zoomOutSize;
        }

    }
}
