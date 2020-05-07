using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beamer : MonoBehaviour
{
    public float radarRange;

    public int radarCycle;

    int radarCountdown;

    public float angularVelo;

    public int damageRate;

    public float deceleration;

    Transform hinge;

	Transform target;

	Transform debris;

	GameObject glow;

    GameObject laser;

	bool laserOn;

    void Start()
    {
        radarCountdown = radarCycle;

        hinge = transform.GetChild(1);

        laser = hinge.GetChild(1).gameObject;

        laser.SetActive(false);

        debris = transform.GetChild(2);

        glow = debris.GetChild(1).gameObject;

        glow.SetActive(false);

        debris.GetComponent<ParticleSystem>().Stop();
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

    void switchOffLaser()
    {
    	if (laserOn) {

			debris.GetComponent<ParticleSystem>().Stop();

			laser.SetActive(false);

			glow.SetActive(false);
		}
		laserOn = false;
    }

    void switchOnLaser()
    {
    	if (!laserOn) {

			debris.GetComponent<ParticleSystem>().Play();

			laser.SetActive(true);

			glow.SetActive(true);
		}
		laserOn = true;
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
    	if (target == null) {

    		switchOffLaser();

    		return;
    	}
    	if (Vector3.Distance(hinge.position, target.position) >= radarRange) {

    		target = null;

    		return;
    	}
    	Quaternion destRotation = Quaternion.LookRotation(target.position - hinge.position);

    	hinge.rotation = Quaternion.RotateTowards(hinge.rotation, destRotation, angularVelo * Time.deltaTime);

    	if (Quaternion.Angle(hinge.rotation, destRotation) >= 5) {

    		switchOffLaser();

    		return;

    	} else {

    		Enemy enemy = target.GetComponent<Enemy>();

    		enemy.takeDamage(damageRate * Time.deltaTime);

    		enemy.takeDebuff(deceleration * Time.deltaTime);

    		switchOnLaser();

    		Vector3 arrow = laser.transform.position - target.position;

    		debris.rotation = Quaternion.LookRotation(arrow);

    		debris.position = target.position + arrow.normalized;

    		float laserNorm = - Vector3.Dot(laser.transform.forward, arrow) / hinge.transform.localScale.z;

    		laser.GetComponent<LineRenderer>().SetPosition(1, Vector3.forward * laserNorm);
    	}
    }
}
