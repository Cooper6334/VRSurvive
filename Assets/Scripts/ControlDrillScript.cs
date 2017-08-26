using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDrillScript : MonoBehaviour
{
	public ParticleSystem fireParticle;
	public ParticleSystem smokeParticle;
	public Transform drillTransform;

	ParticleSystem.MainModule smokeModule;
	ParticleSystem.MainModule fireModule;
	TouchPadUtil touchpadPadUtil;
	int lastRotateCnt;
	Vector3 drillRotateVector = Vector3.zero;
	int targetDrillDegree;
	int currentDrillDegree;

	int drillProgress;
	// Use this for initialization
	void Start ()
	{
		touchpadPadUtil = GetComponent<TouchPadUtil> ();
		smokeModule = smokeParticle.main;
		fireModule = fireParticle.main;
	}
	
	// Update is called once per frame
	void Update ()
	{
		int cnt = touchpadPadUtil.getRotateCount ();
		if (cnt > lastRotateCnt) {
			drillProgress++;
			if (touchpadPadUtil.getRotationDirection () > 0) {
				targetDrillDegree -= 90;
			} else {
				targetDrillDegree += 90;
			}
		}
		lastRotateCnt = cnt;
		updateDrillStatus ();
	}

	void updateDrillStatus ()
	{
		int degreeDiff = Mathf.Abs (targetDrillDegree - currentDrillDegree);
		while (degreeDiff > 360) {
			degreeDiff -= 360;
		}
		int drillMove = 10;
		if (degreeDiff < 10) {
			drillMove = degreeDiff;
		} else {
			drillMove = (int)(10 + degreeDiff / 90 * 2f);
		}
		if (drillMove > 20) {
			drillMove = 20;
		}
		if (targetDrillDegree < currentDrillDegree) {
			currentDrillDegree -= drillMove;
		} else if (targetDrillDegree > currentDrillDegree) {
			currentDrillDegree += drillMove;
		}
		drillRotateVector.y = currentDrillDegree;
		drillTransform.localRotation = Quaternion.Euler (drillRotateVector);

		float smokeSize = drillProgress / 15f;
		if (smokeSize > 3) {
			smokeSize = 3;
		}
		float fireSize = (drillProgress - 60) / 20f;
		if (fireSize > 0.3f) {
			fireSize = 0.3f;
		} else if (fireSize < 0) {
			fireSize = 0;
		}
		smokeModule.startSize = smokeSize;
		fireModule.startSize = fireSize;
	}
}
