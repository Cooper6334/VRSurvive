using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpearScript : MonoBehaviour
{
	public GameObject spearPerfab;
	public Transform head;

	private const float preparSpeed = 0.08f;
	private const int spearSpeedMin = 1500;
	private const int spearSpeedMax = 3500;
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
				spearSpeed = 0;
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
			spearSpeed = -Input.GetAxis ("Mouse X");
			if (spearSpeed > 10) {
				throwSpear ();
				spearPositionReady = false;
				startPress = false;
			}
		}
	}

	void throwSpear ()
	{
		int s = getSpearSpeedValue ();
		spearThrow = true;
		spearObject.transform.parent = null;
		spearObject.GetComponent<Rigidbody> ().AddRelativeForce (0, s, 0);
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

	int getSpearSpeedValue(){
		int s = (int)((spearSpeed - 10) * 100);
		int r = spearSpeedMin + s;
		if (r > spearSpeedMax) {
			return spearSpeedMax;
		}
		return r;
	}
}
