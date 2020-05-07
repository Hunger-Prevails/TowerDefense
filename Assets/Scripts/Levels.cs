using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
	public SceneFader fader;

	public Transform buttonParent;

	void Start()
	{
		var childCount = buttonParent.childCount;

		var unlockCount = PlayerPrefs.GetInt("unlockCount", 1);

        for (int i = 0; i < childCount; i++) {

        	Transform button = buttonParent.GetChild(i);

        	Router router = button.GetComponent<Router>();

        	router.setIndex(i);

        	router.router += selectLevel;

        	if (i >= unlockCount) button.GetComponent<Button>().interactable = false;
        }
	}

    public void selectLevel(int index)
    {
    	fader.Transit(index + 2);
    }
}
