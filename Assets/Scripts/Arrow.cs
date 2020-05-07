using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public float rangeY;

    public float cycle;

    float startY;

    float startAlpha;

    float curTime;

    Image image;

    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();

    	image = GetComponent<Image>();

        startY = rect.anchoredPosition.y + rangeY / 2;

        startAlpha = image.color.a - (1.0f - image.color.a);

        curTime = cycle / 2;
    }

    void Update()
    {
        Vector2 position = rect.anchoredPosition;

        position.y = startY - (curTime / cycle) * rangeY;

        rect.anchoredPosition = position;

        Color color = image.color;

        color.a = (1.0f - startAlpha) * (curTime / cycle) + startAlpha;

        image.color = color;

        curTime = curTime + Time.deltaTime;

        if (curTime > cycle) curTime -= cycle;
    }
}
