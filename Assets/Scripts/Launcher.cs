using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float radarRange;

    public int radarCycle;

    int radarCountdown;

    public float angularVelo;

    public float fireCycle;

    float fireCountdown;

    Transform hinge;

    Transform missileExit;

	Transform target;

	public GameObject missile;

    void Start()
    {
        radarCountdown = radarCycle;

        fireCountdown = 0.5f;

        hinge = transform.GetChild(1);

        missileExit = hinge.GetChild(1);
    }

    void updateTarget()
    {
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    	float closest = radarRange;

    	foreach (var enemy in enemies) {

    		float distance = Vector3.Distance(hinge.position, enemy.transform.position);

    		if (distance < closest) {

    			closest = distance;

    			target = enemy.transform;
    		} 
    	}
    }

    void Update()
    {
    	if (target == null) {

    		radarCountdown --;

	        if (radarCountdown == 0) {

	        	radarCountdown = radarCycle;

	        	updateTarget();
	        }
    	}
    	if (target == null) return;

    	if (Vector3.Distance(hinge.position, target.position) >= radarRange) {

    		target = null;

    		fireCountdown = 0.5f;

    		return;
    	}
    	Quaternion destRotation = Quaternion.LookRotation(target.position - hinge.position);

    	hinge.rotation = Quaternion.RotateTowards(hinge.rotation, destRotation, angularVelo * Time.deltaTime);

    	if (Quaternion.Angle(hinge.rotation, destRotation) >= 5) return;

    	fireCountdown -= Time.deltaTime;

    	if (fireCountdown <= 0) {

    		fireCountdown = fireCycle;

    		fire();
    	}
    }

    void fire()
    {
    	var newMissile = (GameObject)Instantiate(missile, missileExit.position, missileExit.rotation);

    	newMissile.GetComponent<Bullet>().setup(this.target);
    }
}
