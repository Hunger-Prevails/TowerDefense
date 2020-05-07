using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    Image image;

    void Start()
    {
    	image = GetComponentInChildren<Image>();

        StartCoroutine(fadeIn());
    }

    public void Transit(int nextScene)
    {
    	StartCoroutine(fadeOut(nextScene));
    }

    public void Reload()
    {
    	StartCoroutine(fadeOut(SceneManager.GetActiveScene().buildIndex));
    }

    public void Next()
    {
    	var unlockCount = PlayerPrefs.GetInt("unlockCount", 1);

    	PlayerPrefs.SetInt("unlockCount", Mathf.Max(unlockCount, SceneManager.GetActiveScene().buildIndex));

    	StartCoroutine(fadeOut(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator fadeIn()
    {
    	float t = 0.0f;

    	while (t < 1.0f) {

    		Color color = image.color;

    		color.a = 1.0f - t;

    		image.color = color;

    		t += Time.deltaTime;

    		yield return 0;
    	}
    }

    IEnumerator fadeOut(int nextScene)
    {
    	float t = 0.0f;

    	while (t < 1.0f) {

    		Color color = image.color;

    		color.a = t;

    		image.color = color;

    		t += Time.deltaTime;

    		yield return 0;
    	}
    	SceneManager.LoadScene(nextScene);
    }
}
