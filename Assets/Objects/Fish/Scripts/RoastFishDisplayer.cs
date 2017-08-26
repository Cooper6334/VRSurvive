using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoastFishDisplayer : MonoBehaviour
{
	[SerializeField]
	private Renderer renderer;
	private Material mat;

	private float LeftRawWeight
	{
		get {
			return mat.GetFloat ("_LeftRawWeight");
		}

		set {
			mat.SetFloat ("_LeftRawWeight", value);
		}
	}

	private float LeftRipeWeight
	{
		get {
			return mat.GetFloat ("_LeftRipeWeight");
		}

		set {
			mat.SetFloat ("_LeftRipeWeight", value);
		}
	}

	private float LeftBurnedWeight
	{
		get {
			return mat.GetFloat ("_LeftBurnedWeight");
		}

		set {
			mat.SetFloat ("_LeftBurnedWeight", value);
		}
	}

	private float RightRawWeight
	{
		get {
			return mat.GetFloat ("_RightRawWeight");
		}

		set {
			mat.SetFloat ("_RightRawWeight", value);
		}
	}

	private float RightRipeWeight
	{
		get {
			return mat.GetFloat ("_RightRipeWeight");
		}

		set {
			mat.SetFloat ("_RightRipeWeight", value);
		}
	}

	private float RightBurnedWeight
	{
		get {
			return mat.GetFloat ("_RightBurnedWeight");
		}

		set {
			mat.SetFloat ("_RightBurnedWeight", value);
		}
	}

	public void SetLeftRaw()
	{
		LeftRawWeight = 1.0f;
		LeftRipeWeight = 0.0f;
		LeftBurnedWeight = 0.0f;
	}

	public void SetLeftRipe()
	{
		LeftRawWeight = 0.0f;
		LeftRipeWeight = 1.0f;
		LeftBurnedWeight = 0.0f;
	}

	public void SetLeftBurned()
	{
		LeftRawWeight = 0.0f;
		LeftRipeWeight = 0.0f;
		LeftBurnedWeight = 1.0f;
	}

	public void SetRightRaw()
	{
		RightRawWeight = 1.0f;
		RightRipeWeight = 0.0f;
		RightBurnedWeight = 0.0f;
	}

	public void SetRightRipe()
	{
		RightRawWeight = 0.0f;
		RightRipeWeight = 1.0f;
		RightBurnedWeight = 0.0f;
	}

	public void SetRightBurned()
	{
		RightRawWeight = 0.0f;
		RightRipeWeight = 0.0f;
		RightBurnedWeight = 1.0f;
	}

	// Use this for initialization
	void Start ()
	{
		mat = renderer.material;
		LeftRawWeight = 1.0f;
		LeftRipeWeight = 0.0f;
		LeftBurnedWeight = 0.0f;
		RightRawWeight = 1.0f;
		RightRipeWeight = 0.0f;
		RightBurnedWeight = 0.0f;
	}

	/*void Update()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			SetLeftRaw ();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			SetLeftRipe ();
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			SetLeftBurned ();
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			SetRightRaw ();
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			SetRightRipe ();
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			SetRightBurned ();
		}
	}*/
}
