using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
	static BuildManager instance;

	void Awake() { instance = this; }

	public static BuildManager GetInstance() { return instance; }

    public CamControl panControl;

    public GameObject buildEffect;

    public GameObject upgradeEffect;

    public GameObject sellEffect;

    Blueprint blueprint;

    int curBudget;

    public int startBudget;

    public Text wallet;

    public GameObject heartContainer;

    public GameObject heartPrefab;

    public int countHearts;

    public Vector3 heartSpacing;

    public GameObject panelGameOver;

    int curHearts;

    void Start()
    {
    	curBudget = startBudget;

    	wallet.text = "$ " + curBudget.ToString();

    	curHearts = countHearts;

    	StartCoroutine(spawnHearts());
    }

    public void setBlueprint(Blueprint b)
    {
    	blueprint = b;
    }

    public bool canAfford { get { return blueprint.vanillaCost <= curBudget; } }

    public bool canElevate(Blueprint blueprint)
    {
        return blueprint.elevateCost <= curBudget;
    }

    public bool hasBlueprint { get { return blueprint != null; } }

    public GameObject build(Vector3 position)
    {
    	StartCoroutine(changeBudget(curBudget, curBudget - blueprint.vanillaCost));

    	curBudget -= blueprint.vanillaCost;

    	GameObject effect = (GameObject)Instantiate(buildEffect, position, Quaternion.identity);

    	Destroy(effect, 2);

    	return (GameObject)Instantiate(blueprint.vanillaPrefab, position, Quaternion.identity);
    }

    public GameObject upgrade(Vector3 position, Blueprint blueprint)
    {
        StartCoroutine(changeBudget(curBudget, curBudget - blueprint.elevateCost));

        curBudget -= blueprint.elevateCost;

        GameObject effect = (GameObject)Instantiate(upgradeEffect, position, Quaternion.identity);

        Destroy(effect, 2);

        return (GameObject)Instantiate(blueprint.elevatePrefab, position, Quaternion.identity);
    }

    public void sell(Vector3 position, int refund)
    {
        StartCoroutine(changeBudget(curBudget, curBudget + refund));

        curBudget += refund;

        GameObject effect = (GameObject)Instantiate(sellEffect, position, Quaternion.identity);

        Destroy(effect, 2);
    }

    public void killHeart()
    {
    	if (curHearts == 0) return;

    	curHearts --;

    	Destroy(heartContainer.transform.GetChild(curHearts).gameObject);

    	if (curHearts == 0) {

    		panControl.freeze();

    		panelGameOver.SetActive(true);

    		return;
    	}
    }

    public void addBonus(int bonus)
    {
    	StartCoroutine(changeBudget(curBudget, curBudget + bonus));

    	curBudget += bonus;
    }

    IEnumerator changeBudget(int before, int after)
    {
        int divisor = 5;

        if ((after - before) % 10 == 0) divisor = 10;

    	int unit = (after - before) / divisor;

    	for (int i = 0; i < divisor; i++) {

    		before += unit;

    		wallet.text = "$ " + before.ToString();

    		yield return new WaitForSeconds(0.05f);
    	}
    }

    IEnumerator spawnHearts()
    {
    	Vector3 spawnPosition = heartContainer.transform.position;

    	for (int i = 0; i < curHearts; i ++) {

    		Instantiate(heartPrefab, spawnPosition, heartContainer.transform.rotation, heartContainer.transform);

    		spawnPosition += heartSpacing;

    		yield return new WaitForSeconds(0.05f);
    	}
    }

    public Blueprint getBlueprint()
    {
        return blueprint;
    }
}
