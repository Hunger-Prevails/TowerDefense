using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	public float flashDuration;

	public float startIntensity;

	float flashCountdown;

    void Start()
    {
        flashCountdown = flashDuration;
    }

    void Update()
    {
    	flashCountdown = Mathf.Max(0.0f, flashCountdown);

    	GetComponentInChildren<Light>().intensity = flashCountdown / flashDuration * startIntensity;

    	flashCountdown -= Time.deltaTime;
    }
}
