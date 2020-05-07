using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
	public delegate void SelectEvent(ButtonHandler button, Blueprint blueprint);

	public event SelectEvent selection;

	public Blueprint blueprint;

	Color normalColor;

    Button button;

	void Start()
	{
        button = GetComponent<Button>();

		normalColor = GetComponent<Button>().colors.normalColor;
	}

    public void onSelection()
    {
    	selection(this, blueprint);
    }

    public void setColor(Color color)
    {
    	ColorBlock block = button.colors;

	    block.normalColor = color;

	    button.colors = block;
    }

    public void resetColor()
    {
    	ColorBlock block = button.colors;

	    block.normalColor = normalColor;

	    button.colors = block;
    }
}
