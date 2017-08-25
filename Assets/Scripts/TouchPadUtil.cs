using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPadUtil : MonoBehaviour {
	
	bool touching;
	int circleDirection;
	int lastDirection;
	int circleCount;
	float minx,miny,maxx,maxy;

	// Use this for initialization
	void Start () {
		minx = float.MaxValue;
		miny = float.MaxValue;
		maxx = float.MinValue;
		maxy = float.MinValue;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			touching = false;
		} else if(Input.GetMouseButtonDown(0)){
			touching = true;
		}
		if (!touching) {
			return;
		}
		float moveX = Input.GetAxis ("Mouse X");
		float moveY = Input.GetAxis ("Mouse Y");
		checkCircle (moveX, moveY);
	}

	public bool isTouching(){
		return touching;
	}

	public int getRotationDirection(){
		if(circleCount > 3){
			return 1;
		} else if(circleCount < -3){
			return -1;
		} else {
			return 0;
		}
	}

//	    1->
//  4^	0     2v
//  	3<-
	private void checkCircle(float x,float y){
		if (x == 0 && y == 0) {
			lastDirection = 0;
			circleDirection = 0;
			circleCount = 0;
			return;
		}
		int direction;
		if (Mathf.Abs (x) > Mathf.Abs (y)) {
			if (x > 0) {
				direction = 1;
			} else {
				direction = 3;
			}
		} else {
			if (y > 0) {
				direction = 4;
			} else {
				direction = 2;
			}
		}
		if (direction == lastDirection) {
			return;
		}
		int directionDiff = direction - lastDirection;
		if (direction == 4 && lastDirection == 1) {
			directionDiff = -1;
		} else if (direction == 1 && lastDirection == 4) {
			directionDiff = 1;
		}
		if (circleCount >= 0 && directionDiff == 1) {
			circleCount++;
		} else if (circleCount <= 0 && directionDiff == -1) {
			circleCount--;
		} else {
			circleCount = 0;
		}
		lastDirection = direction;
	}
}
