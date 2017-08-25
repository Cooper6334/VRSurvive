using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextLevel : MonoBehaviour
{
	void Start ()
	{
		LevelManager.Instance.GoToNextLevel ();
	}
}
