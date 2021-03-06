﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemController : MonoBehaviour {
	public GameObject camera;
	public GameObject reticle;
	public GameObject[] iconPrefab;

	GameObject showingIcon;
	System.DateTime showIconTime = System.DateTime.MinValue;
	Vector3 iconPosition = new Vector3(0,0,0.15f);

	void Update(){
		if (showingIcon == null) {
			return;
		}
		//if ((System.DateTime.Now - showIconTime).TotalSeconds > 1) {
		if ((System.DateTime.Now - showIconTime).TotalSeconds > 1) {
			Destroy (showingIcon);
			if (reticle != null) {
				reticle.GetComponent<MeshRenderer> ().enabled = true;
			}
		}
	}

	public void showIcon(int id, Vector3 position){
		if (showingIcon != null) {
			Destroy (showingIcon);
		}
		showingIcon = Instantiate (iconPrefab [id]);
		//showingIcon.transform.parent = camera.transform;
		//showingIcon.transform.localRotation = Quaternion.Euler (Vector3.zero);
		//showingIcon.transform.localPosition = iconPosition;
		showingIcon.transform.position = position;
		showingIcon.transform.forward = position - camera.transform.position;
		showingIcon.transform.localScale *= (position - camera.transform.position).magnitude / 0.15f;
		showIconTime = System.DateTime.Now;
		if (reticle != null) {
			reticle.GetComponent<MeshRenderer> ().enabled = false;
		}
	}

}
