using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{
	public delegate void RouteEvent(int level);

	public event RouteEvent router;

	int buttonIndex;

	public void setIndex(int index)
	{
		buttonIndex = index;
	}

	public void onClick()
	{
		router(buttonIndex);
	}
}
