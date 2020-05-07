using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] waypoints;

    void Awake()
    {
    	var childCount = transform.childCount;

        waypoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++) waypoints[i] = transform.GetChild(i);
    }
}
