using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public delegate void DeathEvent();

    public event DeathEvent death;

    public float topSpeed;

    public float acceleration;

    public int fullHealth;

    public int killBonus;

    public GameObject deathEffect;

	float speed;

	Transform target;

    float curHealth;

	int waypointIndex;

    bool onDebuff;

    RectTransform rectTrans;

    Image image;

    float fullWidth;

    bool dead;

    void Start()
    {
        waypointIndex = 0;

        target = Waypoints.waypoints[waypointIndex];

        curHealth = fullHealth;

        speed = topSpeed;

        var healthBar = transform.GetChild(0).GetChild(1);

        image = healthBar.GetComponent<Image>();

        rectTrans = healthBar.GetComponent<RectTransform>();

        fullWidth = rectTrans.sizeDelta.x;
    }

    public void takeDamage(float damage)
    {
        if (dead) return;

        curHealth -= damage;

        Vector2 size = rectTrans.sizeDelta;

        float portion = curHealth / fullHealth;

        size.x = fullWidth * portion;

        rectTrans.sizeDelta = size;

        image.color = new Color(1 - Mathf.Max(portion, 0.5f), Mathf.Min(portion, 0.5f), 0.0f) * 2.0f;

        if (curHealth <= 0) {

            dead = true;

            death();

            BuildManager.GetInstance().addBonus(killBonus);

            GameObject debris = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);

            Destroy(debris, 5);

            Destroy(gameObject);
        }
    }

    public void takeDebuff(float deceleration)
    {
        onDebuff = true;

        speed = Mathf.Max(topSpeed * 0.5f, speed - deceleration);
    }

    void Update()
    {
    	if (Vector3.Distance(target.position, transform.position) <= 0.1f) {

    		waypointIndex ++;

    		if (waypointIndex == Waypoints.waypoints.Length) {

                death();

                BuildManager.GetInstance().killHeart();

    			Destroy(gameObject);

    			return;
    		}
    		target = Waypoints.waypoints[waypointIndex];
    	}
        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (!onDebuff) speed = Mathf.Min(speed + acceleration * Time.deltaTime, topSpeed);

        else onDebuff = false;
    }
}
