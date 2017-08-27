using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRoastScript : MonoBehaviour
{
	public GameObject[] fishStick;

	GetItemController getItemController;
	RoastFishStatus[] fishStatus;
	TouchPadUtil touchPadUtil;
	int fishCnt;
	int currentFish;
	bool selectFish;
	int successFishCnt;
	int burnFishCnt;

	// Use this for initialization
	void Start ()
	{
		getItemController = GetComponent<GetItemController> ();
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
		if (burnFishCnt + successFishCnt >= 3) {
			// ending
			return;
		}
		if (currentFish >= 0) {
			if (Input.GetMouseButtonDown (0)) {
				selectFish = true;
			} else if (Input.GetMouseButtonUp (0)) {
				int fishResult = fishStatus [currentFish].getFish ();
				if (selectFish && fishResult > 0) {
					if (fishResult == 1) {
						successFishCnt++;
						getItemController.showIcon (0, new Vector3(0.0f, 8.0f, 0.0f));
					} else {
						burnFishCnt++;
						getItemController.showIcon (1, new Vector3(0.0f, 8.0f, 0.0f));
					}
					fishStick [currentFish].SetActive (false);
					currentFish = -1;
				}
			}
			if (touchPadUtil.getRotateCount () != 0) {
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

		if (Input.GetKeyDown (KeyCode.F)) {
			getItemController.showIcon (1, new Vector3(0.0f, 8.0f, 0.0f));
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
		int targetDegree;
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
			targetDegree += 180;
			while (targetDegree > currentDegree + 360) {
				targetDegree -= 360;
			}
		}

		public int getFish ()
		{
			if (isFinish) {
				return 0;
			}
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
			if (currentDegree < targetDegree) {
				currentDegree += 10;
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
