﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercam : MonoBehaviour {
  private const float Y_ANGLE_MIN = 15.0f;
  private const float Y_ANGLE_MAX = 91.0f;

  // private const float X_ANGLE_MIN = 0.0f;
  //private const float X_ANGLE_MAX = 45;

  public Transform lookAt;
  public Transform camTransform { get; set; }

  public Camera cam;

  private float distance = 5.0f;
  private float currentX = 15.0f;
  private float currentY = 60.0f;
  private float sensitivityX = 2.0f;
  private float sensitivityY = 1.0f;  

  private void Start()
  {
    camTransform = transform;
    cam = Camera.main;
  }

  private void Update()
  {
    //currentX += Input.GetAxis("Mouse X");
    //currentY += Input.GetAxis("Mouse Y");

    currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
  }

  private void LateUpdate()
  {
    Vector3 dir = new Vector3(0, 0, -distance);
    Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
    camTransform.position = lookAt.position + rotation * dir;

    camTransform.LookAt(lookAt.position);
  }
}
