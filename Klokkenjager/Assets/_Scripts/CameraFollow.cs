﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[Header("General Variables")]
	public float CameraMoveSpeed = 120.0f;
	public GameObject CameraFollowObj;
	Vector3 FollowPOS;
	public float clampAngle = 80.0f;
	public float inputSensitivity = 150.0f;
	public GameObject CameraObj;
	public GameObject PlayerObj;
    public float playerRotationSpeed = 3f;
    public float camDistanceXToPlayer;
	public float camDistanceYToPlayer;
	public float camDistanceZToPlayer;
	public float mouseX;
	public float mouseY;
	public float inputX; 
	public float inputZ;
	public float finalInputX;
	public float finalInputZ;
	public float smoothX;
	public float smoothY;
	public bool cameraControlsDisabled;

	private float rotY = 0.0f;
	private float rotX = 0.0f;

	// Use this for initialization
	void Start() {

		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
		inputX = Input.GetAxis ("RightStickHorizontal");
		inputZ = Input.GetAxis ("RightStickVertical");
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update() {

		if (cameraControlsDisabled == false) {
			// We setup the rotation of the sticks here
			mouseX = Input.GetAxis ("Mouse X");
			mouseY = Input.GetAxis ("Mouse Y");
			finalInputX = inputX + mouseX;
			finalInputZ = inputZ + mouseY;

			rotY += finalInputX * inputSensitivity * Time.deltaTime;
			rotX += finalInputZ * inputSensitivity * Time.deltaTime;

			rotX = Mathf.Clamp (rotX, -clampAngle, clampAngle);

			Quaternion localRotation = Quaternion.Euler (rotX, rotY, 0.0f);
			transform.rotation = localRotation;
			PlayerObj.transform.rotation = Quaternion.Lerp(PlayerObj.transform.rotation, Quaternion.Euler(0, rotY, 0), Time.deltaTime * playerRotationSpeed);
		}
	}

	void LateUpdate() {
		CameraUpdater();
	}

	void CameraUpdater() {
		// set the target object to follow
		Transform target = CameraFollowObj.transform;

		//move towards the game object that is the target
		float step = CameraMoveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}
}
