using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTurret : MonoBehaviour
{
	public Transform target;

	public float radarRange;

	public float angularVelo;

	Transform hinge;

    void Start()
    {
        hinge = transform.GetChild(1);
    }

    void Update()
    {
    	if (Vector3.Distance(hinge.position, target.position) >= radarRange) return;

        Quaternion destRotation = Quaternion.LookRotation(target.position - hinge.position);

        float angle_y = destRotation.eulerAngles.y - hinge.rotation.eulerAngles.y;

        if (Mathf.Abs(angle_y) > 180) angle_y -= Mathf.Sign(angle_y) * 360;

        angle_y = Mathf.Sign(angle_y) * Mathf.Min(Mathf.Abs(angle_y), angularVelo * Time.deltaTime);

        hinge.Rotate(Vector3.up * angle_y);
    }
}
