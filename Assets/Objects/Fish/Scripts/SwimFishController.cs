using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimFishController : MonoBehaviour
{
	public Vector3 speed;
	private Vector3 originPos;

	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<FishResetTrigger> () != null) {
			transform.position = originPos;
		}
	}

	void Start ()
	{
		originPos = transform.position;
	}

	void Update ()
	{
		transform.position += speed * Time.deltaTime;
	}
}
