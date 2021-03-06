﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCollisionScript : MonoBehaviour {

	public ControlSpearScript controlSpear;

	public void setControlSpearScript(ControlSpearScript s){
		controlSpear = s;
	}

	public void OnCollisionEnter(Collision collision){
		Destroy (this.gameObject);
	}

	public void OnTriggerEnter(Collider collider){
		if (collider.tag != "Fish") {
			return;
		}
		Debug.Log ("spear hit fish");
		controlSpear.hitFish ();
		Destroy (this.gameObject);
		Destroy (collider.gameObject);
	}
}
