using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpearScript : MonoBehaviour
{
	public GameObject spearPerfab;
	public Transform head;

	private const float preparSpeed = 0.08f;
	private const int spearSpeedMin = 2500;
	private const int gravityForce = -25;
	private const int rotateForce = 100;

	GameObject spearObject;

	bool startPress;
	bool spearPositionReady;
	float spearSpeed;
	bool spearThrow;
	Vector3 initSpearPosition = new Vector3 (1.5f, 0, 0);

	// Use this for initialization
	void Start ()
	{
		initSpearObject ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (spearThrow) {
			if (spearObject != null) {
				spearObject.GetComponent<Rigidbody> ().AddForce (0, gravityForce, 0);
			} else if (Input.GetMouseButtonDown (0)) {
				initSpearObject ();
			}
			return;
		}
		if (Input.GetMouseButtonDown (0)) {
			startPress = true;
			spearPositionReady = false;
		} else if (Input.GetMouseButtonUp (0)) {
			if (startPress && spearPositionReady) {
				throwSpear ();
			}
			spearPositionReady = false;
			startPress = false;
		}
		if (!spearPositionReady) {
			Vector3 spearPosition = spearObject.transform.localPosition;
			if (startPress) {
				spearPosition.z -= preparSpeed;
				if (spearPosition.z <= -3) {
					spearPosition.z = -3;
					spearPositionReady = true;
				} 
			} else {
				spearPosition.z += preparSpeed;
				if (spearPosition.z >= 0) {
					spearPosition.z = 0;
					spearPositionReady = true;
				} 
			}
			spearObject.transform.localPosition = spearPosition;
		}
		if (startPress && spearPositionReady) {
			spearSpeed = Input.GetAxis ("Mouse X");
		}
	}

	void throwSpear ()
	{
		if (spearSpeed < 0) {
			return;
		}
		spearThrow = true;
		spearObject.transform.parent = null;
		spearObject.GetComponent<Rigidbody> ().AddRelativeForce (0, spearSpeedMin + spearSpeed * 20, 0);
		spearObject.GetComponent<Rigidbody> ().AddTorque (spearObject.transform.forward * rotateForce, ForceMode.Force);
	}

	void initSpearObject ()
	{
		spearThrow = false;
		spearObject = Instantiate (spearPerfab);
		spearObject.transform.parent = head;
		spearObject.transform.localPosition = initSpearPosition;
		spearObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 90, 90));
	}
}
