using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public delegate void SelectEvent(Node node);

    public event SelectEvent selection;

	Renderer painter;

	Color normalColor;

	Blueprint blueprint;

    GameObject turretBuild;

    bool elevation;

	Vector3 offset;

	bool curSelection;

	public Color hoverBuildDeny;

    public Color hoverBuildGrant;

    public Color hoverSelect;

    public Color underSelect;

    public bool canElevate { get { return !elevation; } }

	void Start()
	{
		painter = GetComponent<Renderer>();

		normalColor = painter.material.color;

		offset = new Vector3(0.0f, 0.25f, 0.0f);
	}

    void OnMouseEnter()
    {
    	if (curSelection) return;

    	if (turretBuild != null) {

    		painter.material.color = hoverSelect;

    	} else if (BuildManager.GetInstance().hasBlueprint) {

    		if (BuildManager.GetInstance().canAfford) painter.material.color = hoverBuildGrant;

    		else painter.material.color = hoverBuildDeny;
    	}
    }

    void OnMouseExit()
    {
    	if (curSelection) return;

    	painter.material.color = normalColor;
    }

    void OnMouseDown()
    {
        BuildManager manager = BuildManager.GetInstance();

    	if (turretBuild != null) {

    		if (curSelection) {

    			curSelection = false;

    			painter.material.color = hoverSelect;

    		} else {

    			curSelection = true;

    			painter.material.color = underSelect;
    		}
    		selection(this);
    	
    	} else {

	    	if (!manager.hasBlueprint) return;

	    	if (!manager.canAfford) return;

            blueprint = manager.getBlueprint();

	    	turretBuild = manager.build(transform.position + offset);

	    	painter.material.color = hoverSelect;
    	}
    }

    public void deselect()
    {
    	curSelection = false;

    	painter.material.color = normalColor;
    }

    public int getElevateCost()
    {
        return blueprint.elevateCost;
    }

    public int getSellRefund()
    {
        int refund = blueprint.vanillaCost / 2;

        if (elevation) refund += blueprint.elevateCost / 2;

        return refund;
    }

    public bool upgrade()
    {
        BuildManager manager = BuildManager.GetInstance();

        if (!manager.canElevate(blueprint)) return false;

        turretBuild.SetActive(false);

        Destroy(turretBuild);

        turretBuild = manager.upgrade(transform.position + offset, blueprint);

        deselect();

        elevation = true;

        return true;
    }

    public void sell()
    {
        BuildManager manager = BuildManager.GetInstance();

        turretBuild.SetActive(false);

        Destroy(turretBuild);

        manager.sell(transform.position + offset, getSellRefund());

        blueprint = null;

        deselect();

        elevation = false;
    }
}
