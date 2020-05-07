using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattyLauncher : MonoBehaviour
{
    public float radarRange;

    public int radarCycle;

    int radarCountdown;

    public float angularVelo;

    public float fireCycle;

    float fireCountdown;

    Transform body;

    Transform hinge;

    Transform missileExit;

	Transform target;

	public GameObject missile;

    void Start()
    {
        radarCountdown = radarCycle;

        fireCountdown = 0.5f;

        body = transform.GetChild(1);

        hinge = body.GetChild(1);

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
    	Quaternion destRotation = Quaternion.LookRotation(target.position - body.position);

        float angle_y = destRotation.eulerAngles.y - body.rotation.eulerAngles.y;

        if (Mathf.Abs(angle_y) > 180) angle_y -= Mathf.Sign(angle_y) * 360;

        angle_y = Mathf.Sign(angle_y) * Mathf.Min(Mathf.Abs(angle_y), angularVelo * Time.deltaTime);

        body.Rotate(Vector3.up * angle_y);

    	destRotation = Quaternion.LookRotation(target.position - hinge.position);

        float angle_x = destRotation.eulerAngles.x - hinge.rotation.eulerAngles.x;

    	if (Mathf.Abs(angle_x) > 180) angle_x -= Mathf.Sign(angle_x) * 360;

        angle_x = Mathf.Sign(angle_x) * Mathf.Min(Mathf.Abs(angle_x), angularVelo * Time.deltaTime);

        hinge.Rotate(Vector3.right * angle_x);

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
