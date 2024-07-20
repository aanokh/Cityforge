using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Alexander Anokhin

public class CameraController : MonoBehaviour {

    public const int RIGHT_BOUNDARY = 1000;
    public const int LEFT_BOUNDARY = -1000;
    public const int TOP_BOUNDARY = 1000;
    public const int BOTTOM_BOUNDARY = -1000;
    

    private Vector3 mouseLockPos;

    private Camera myCamera;

    public void Start() {
        myCamera = GetComponent<Camera>();
    }

    public void Update() {
        if (Input.GetMouseButtonDown(1)) {
            mouseLockPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1)) {
            if (Input.mousePosition != mouseLockPos) {

                Vector3 deltaPos = myCamera.ScreenToWorldPoint(mouseLockPos) - myCamera.ScreenToWorldPoint(Input.mousePosition);

                Vector3 newpos = transform.position;

                if (newpos.x + deltaPos.x < LEFT_BOUNDARY) {
                    newpos.x = LEFT_BOUNDARY;
                } else if (newpos.x + deltaPos.x > RIGHT_BOUNDARY) {
                    newpos.x = RIGHT_BOUNDARY;
                } else {
                    newpos.x += deltaPos.x;
                }

                if (newpos.y + deltaPos.y < BOTTOM_BOUNDARY) {
                    newpos.y = BOTTOM_BOUNDARY;
                } else if (newpos.y + deltaPos.y > TOP_BOUNDARY) {
                    newpos.y = TOP_BOUNDARY;
                } else {
                    newpos.y += deltaPos.y;
                }

                transform.position = newpos;

                mouseLockPos = Input.mousePosition;
            }
        }
    }
}
