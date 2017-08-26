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
	GetItemController getItemController;

	bool startPress;
	bool spearPositionReady;
	float spearSpeed;
	bool spearThrow;
	int hitFishCnt;
	Vector3 initSpearPosition = new Vector3 (1.5f, 0, 0);

	// Use this for initialization
	void Start ()
	{
		getItemController = GetComponent<GetItemController> ();
		initSpearObject ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (spearObject == null) {
			if (Input.GetMouseButtonDown (0)) {
				initSpearObject ();
			} else {
				return;
			}
		}
		if (spearThrow) {
			spearObject.GetComponent<Rigidbody> ().AddForce (0, gravityForce, 0);
			return;
		}
		// spear on hand
		if (Input.GetMouseButtonDown (0)) {
			startPress = true;
			spearPositionReady = false;
		} else if (Input.GetMouseButtonUp (0)) {
			spearSpeed = 0;
			spearPositionReady = false;
			startPress = false;
			return;
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
			float newSpeed = -Input.GetAxis ("Mouse X");
			if (newSpeed > spearSpeed) {
				spearSpeed = newSpeed;
			}
			throwSpear();
		}
	}

	void throwSpear ()
	{
		if (spearSpeed < 10) {
			return;
		}
		int s = getSpearSpeedValue ();
		spearThrow = true;
		spearObject.transform.parent = null;
		spearObject.GetComponent<Rigidbody> ().AddRelativeForce (0, s, 0);
		spearObject.GetComponent<Rigidbody> ().AddTorque (spearObject.transform.forward * rotateForce, ForceMode.Force);
		spearSpeed = 0;
	}

	void initSpearObject ()
	{
		spearThrow = false;
		spearObject = Instantiate (spearPerfab);
		spearPerfab.GetComponent<SpearCollisionScript> ().setControlSpearScript (this);
		spearObject.transform.parent = head;
		spearObject.transform.localPosition = initSpearPosition;
		spearObject.transform.localRotation = Quaternion.Euler (new Vector3 (0, 90, 90));
	}

	int getSpearSpeedValue ()
	{
		int s = (int)((spearSpeed - 10) * 100);
		int r = spearSpeedMin + s;
		if (r > spearSpeedMax) {
			return spearSpeedMax;
		}
		return r;
	}

	public void hitFish ()
	{
		hitFishCnt++;
		getItemController.showIcon (0);
		if (hitFishCnt >= 3) {
			LevelManager.Instance.GoToNextLevel (true);
		}
	}
}
