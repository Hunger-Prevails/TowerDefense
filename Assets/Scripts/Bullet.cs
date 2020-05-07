using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	Transform target;

	public float speed;

	public float angularVelo;

	public float damageRange;

	public int damageAmount;

    public float hitRange;

	public GameObject impact;

	public void setup(Transform t)
	{
		target = t;
	}

    void Update()
    {
        if (target == null) {

        	Destroy(gameObject);

        	return;
        }
        Vector3 arrow = target.position - transform.position;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(arrow), angularVelo * Time.deltaTime);

        transform.Translate(arrow.normalized * speed * Time.deltaTime, Space.World);

        float curDist = Vector3.Distance(target.position, transform.position);

        if (curDist <= hitRange) knock();
    }

    void knock()
    {
    	GameObject shrapnel = Instantiate(impact, transform.position, transform.rotation);

    	Destroy(shrapnel, 5);

    	if (damageRange != 0) {

    		Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);

    		foreach (Collider collider in colliders) {

    			if (collider.gameObject.CompareTag("Enemy")) damage(collider.gameObject);
    		}

		} else {

			damage(target.gameObject);
		}

    	Destroy(gameObject);
    }

    void damage(GameObject enemy)
    {
    	enemy.GetComponent<Enemy>().takeDamage(damageAmount);
    }
}
