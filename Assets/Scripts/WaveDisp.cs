using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class WaveDisp : MonoBehaviour
{
	void OnEnable()
	{
		GetComponent<Text>().text = Spawner.GetInstance().getRepelWaves().ToString();
	}
}
