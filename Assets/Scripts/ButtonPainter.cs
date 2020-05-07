using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonPainter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public float maxScale;

	public int steps;

	public int colorSteps;

	Button button;

	RectTransform rectTrans;

	bool highlight;

	int counter;

	float curScale;

	Color startColor;

	Color destColor;

	Color unitColor;

	Color curColor;

	int colorCounter;

	bool colorUp;

	void Start()
	{
		button = GetComponent<Button>();

		startColor = button.colors.normalColor;

		destColor = button.colors.pressedColor;

		unitColor = (destColor - startColor) / colorSteps;

		rectTrans = GetComponent<RectTransform>();
	}

	void Update()
	{
		if (highlight) {

			if (counter < steps) {

				curScale += (maxScale - 1.0f) / steps;

				counter ++;

				rectTrans.localScale = new Vector3(curScale, curScale, curScale);
			}
			if (colorUp) curColor += unitColor;

			else curColor -= unitColor;

			ColorBlock block = button.colors;

			block.highlightedColor = curColor;

			button.colors = block;

			colorCounter ++;

			if (colorCounter == colorSteps) {

				colorUp = !colorUp;

				colorCounter = 0;
			}
		
		} else {

			if (counter != 0) {

				curScale -= (maxScale - 1.0f) / steps;

				counter --;

				rectTrans.localScale = new Vector3(curScale, curScale, curScale);
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		highlight = true;

		curScale = 1.0f;

		curColor = startColor;

		colorCounter = 0;

		colorUp = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		highlight = false;
	}
}
