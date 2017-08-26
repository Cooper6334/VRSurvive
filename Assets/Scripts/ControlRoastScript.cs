using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRoastScript : MonoBehaviour {
	public GameObject[] fishStick;

	int fishCnt;
	int tmp;
	RoastFishStatus[] fishStatus;
	// Use this for initialization
	void Start () {
		Debug.Log ("finish init fish start");
		fishCnt = fishStick.Length;
		fishStatus = new RoastFishStatus[fishCnt];
		for(int i=0;i<fishCnt;i++){
			Transform stick = fishStick[i].transform.GetChild (0);
			RoastFishDisplayer displayer = stick.GetChild (0).GetComponent<RoastFishDisplayer> ();
			fishStatus[i] = new RoastFishStatus(displayer,stick);
		}
		Debug.Log ("finish init fish");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			fishStatus [tmp].change ();
			tmp = (tmp + 1) % fishCnt;
		}
		foreach (RoastFishStatus status in fishStatus) {
				if(!status.rotate()){
				status.addRoast ();
			}
		}
	}

	class RoastFishStatus{
		Vector3 stickRotation;
		RoastFishDisplayer fish;
		Transform stick;
		float aPartStatus;
		float bPartStatus;
		bool isBPart;
		int currentDegree;

		public RoastFishStatus(RoastFishDisplayer f,Transform t){
			fish = f;
			stick = t;
		}
		public void change(){
			isBPart = !isBPart;
		}

		public bool rotate(){
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

		public void addRoast(){
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
