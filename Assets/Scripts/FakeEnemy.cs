using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemy : MonoBehaviour
{
	public float speed;

	Transform target;

	int next;

    void Start()
    {
        next = 0;

        target = Waypoints.waypoints[next];
    }

    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= 0.1f) {

    		next = (next + 1) % Waypoints.waypoints.Length;

    		target = Waypoints.waypoints[next];
    	}
        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }
}
