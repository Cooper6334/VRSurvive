using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	private static readonly int LevelNumber = 3;
	private static LevelManager instance;

	private int currentLevel;

	public static LevelManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<LevelManager> ();
			}

			if (instance == null) {
				Debug.LogError ("There is no SceneManager in the scene.");
			}

			return instance;
		}
	}

	public void GoToLevel(int levelNumber)
	{
		currentLevel = Mathf.Clamp(levelNumber, 0, LevelNumber);
		SceneManager.LoadScene (currentLevel);
	}

	public void GoToNextLevel()
	{
		currentLevel = Mathf.Clamp(currentLevel + 1, 0, LevelNumber);
		SceneManager.LoadScene (currentLevel);
	}

	void Start ()
	{
		currentLevel = 0;
	}
}
