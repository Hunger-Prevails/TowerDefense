using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Inspector : MonoBehaviour
{
	public float rangeY;

    public int steps;

    float startY;

    bool flag;

    RectTransform rect;

    void Start()
    {
    	rect = GetComponent<RectTransform>();

        startY = rect.anchoredPosition.y - rangeY;

        StartCoroutine(fadeIn());

        flag = true;
    }

    void OnEnable()
    {
		if (flag) StartCoroutine(fadeIn());
    }

    IEnumerator fadeIn()
    {
    	float unitY = rangeY / steps;

    	for (int i = 0; i < steps; i ++) {

    		Vector2 position = rect.anchoredPosition;

    		position.y = (i + 1) * unitY + startY;

    		rect.anchoredPosition = position;

    		yield return new WaitForSeconds(0.05f);
    	}
    }
}
