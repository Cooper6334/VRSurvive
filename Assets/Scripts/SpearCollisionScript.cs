using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCollisionScript : MonoBehaviour {

	public void OnCollisionEnter(Collision collision){
		Debug.Log ("spear collision");
		Destroy (this.gameObject);
	}
}
