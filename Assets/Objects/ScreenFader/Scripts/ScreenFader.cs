using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
	public Material mat;

	private float fadeSpeed = 1.5f;

	private static ScreenFader instance;
	private IEnumerator fadeInCoroutine;
	private IEnumerator fadeOutCoroutine;

	public static ScreenFader Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<ScreenFader> ();
			}

			if (instance == null) {
				Debug.LogError ("There is no ScreenFader in the scene.");
			}

			return instance;
		}
	}

	public float FadeInWeight
	{
		get
		{
			if (mat != null) {
				return mat.GetFloat ("_FadeInWeight");
			} else {
				return 1.0f;
			}
		}

		set {
			if (mat != null) {
				mat.SetFloat ("_FadeInWeight", Mathf.Clamp01(value));
			}
		}
	}

	public float Saturation
	{
		get
		{
			if (mat != null) {
				return 1.0f - mat.GetFloat ("_BwBlend");
			} else {
				return 1.0f;
			}
		}

		set {
			if (mat != null) {
				mat.SetFloat ("_BwBlend", Mathf.Clamp01(1.0f - value));
			}
		}
	}

	public IEnumerator FadeOutScreenCoroutine()
	{
		float fadeInWeight = 1.0f;
		float saturation = 1.0f;

		while (fadeInWeight > 0.0f || saturation > 0.0f) {
			FadeInWeight = Mathf.Clamp01 (fadeInWeight);
			Saturation = Mathf.Clamp01 (saturation);
			fadeInWeight -= fadeSpeed * Time.deltaTime;
			saturation -= fadeSpeed * Time.deltaTime;
			yield return null;
		}
	}

	public IEnumerator FadeInScreenCoroutine()
	{
		float fadeInWeight = 0.0f;
		float saturation = 0.0f;

		while (fadeInWeight < 1.0f || saturation < 1.0f) {
			FadeInWeight = Mathf.Clamp01 (fadeInWeight);
			Saturation = Mathf.Clamp01 (saturation);
			fadeInWeight += fadeSpeed * Time.deltaTime;
			saturation += fadeSpeed * Time.deltaTime;
			yield return null;
		}
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, mat);
	}

	void Start()
	{
		if (LevelManager.Instance.IsFadeOutScreen) {
			StartCoroutine (FadeInScreenCoroutine());
		}
	}
}
