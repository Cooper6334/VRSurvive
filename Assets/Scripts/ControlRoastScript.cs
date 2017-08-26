using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRoastScript : MonoBehaviour
{
	public GameObject[] fishStick;

	RoastFishStatus[] fishStatus;
	TouchPadUtil touchPadUtil;
	int fishCnt;
	int currentFish;
	bool selectFish;

	// Use this for initialization
	void Start ()
	{
		touchPadUtil = GetComponent<TouchPadUtil> ();
		fishCnt = fishStick.Length;
		fishStatus = new RoastFishStatus[fishCnt];
		for (int i = 0; i < fishCnt; i++) {
			Transform stick = fishStick [i].transform.GetChild (0);
			RoastFishDisplayer displayer = stick.GetChild (0).GetComponent<RoastFishDisplayer> ();
			fishStatus [i] = new RoastFishStatus (displayer, stick);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentFish >= 0) {
			if (Input.GetMouseButtonDown (0)) {
				selectFish = true;
			} else if (Input.GetMouseButtonUp (0)) {
				if (selectFish && fishStatus [currentFish].getFish () > 0) {
					fishStick [currentFish].SetActive (false);
				}
			}
			if (touchPadUtil.getRotateCount() != 0) {
				touchPadUtil.resetRotate ();
				fishStatus [currentFish].change ();
				selectFish = false;
			}
		} else {
			selectFish = false;
		}
		foreach (RoastFishStatus status in fishStatus) {
			if (!status.rotate ()) {
				status.addRoast ();
			}
		}
	}

	public void SetCurrentFish (int f)
	{
		currentFish = f;
	}

	class RoastFishStatus
	{
		Vector3 stickRotation;
		RoastFishDisplayer fish;
		Transform stick;
		float aPartStatus;
		float bPartStatus;
		bool isBPart;
		int currentDegree;
		bool isFinish;

		public RoastFishStatus (RoastFishDisplayer f, Transform t)
		{
			fish = f;
			stick = t;
		}

		public void change ()
		{
			isBPart = !isBPart;
		}

		public int getFish ()
		{
			if (aPartStatus > 2.5f || bPartStatus > 2.5f) {
				isFinish = true;
				return 2;
			} else if (aPartStatus > 1.5f && bPartStatus > 1.5f) {
				isFinish = true;
				return 1;
			}
			return 0;
		}

		public bool rotate ()
		{
			int target = isBPart ? 180 : 0;
			if (currentDegree < target) {
				currentDegree += 10;
			} else if (currentDegree > target) {
				currentDegree -= 10;
			} else {
				return false;
			}
			stickRotation.y = currentDegree;
			stick.localRotation = Quaternion.Euler (stickRotation);
			return true;
		}

		public void addRoast ()
		{
			float addValue = 0.001f;
			if (isBPart) {
				bPartStatus += addValue;
				aPartStatus += addValue * 3;
			} else {
				bPartStatus += addValue * 3;
				aPartStatus += addValue;
			}
			if (aPartStatus > 2.5f) {
				fish.SetRightBurned ();
			} else if (aPartStatus > 1.5f) {
				fish.SetRightRipe ();
			}
			if (bPartStatus > 2.5f) {
				fish.SetLeftBurned ();
			} else if (bPartStatus > 1.5f) {
				fish.SetLeftRipe ();
			}
		}
	}
}
