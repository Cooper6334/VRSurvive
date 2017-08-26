using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	private static readonly int LevelNumber = 3;
	private static LevelManager instance;

	private int currentLevel;
	public bool isFadeOutScreen;

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

	public bool IsFadeOutScreen
	{
		get {
			return isFadeOutScreen;
		}
	}

	public void GoToLevel(int levelNumber, bool fadeOutScreen = false)
	{
		currentLevel = Mathf.Clamp(levelNumber, 0, LevelNumber);
		this.isFadeOutScreen = fadeOutScreen;
		StartCoroutine (GoToLevelCoroutine(currentLevel));
	}

	public void GoToNextLevel(bool fadeOutScreen = false)
	{
		if (currentLevel + 1 > LevelNumber) {
			currentLevel = 0;
		}
		currentLevel = Mathf.Clamp(currentLevel + 1, 0, LevelNumber);
		this.isFadeOutScreen = fadeOutScreen;
		StartCoroutine (GoToLevelCoroutine(currentLevel));
	}

	private IEnumerator GoToLevelCoroutine(int levelNumber)
	{
		if (isFadeOutScreen) {
			yield return StartCoroutine (ScreenFader.Instance.FadeOutScreenCoroutine ());
		}

		SceneManager.LoadScene(levelNumber);

		yield return null;
	}

	void Awake ()
	{
		currentLevel = 0;
		isFadeOutScreen = false;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.N)) {
			LevelManager.instance.GoToNextLevel (true);
		}
	}
}
