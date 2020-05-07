using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	ButtonHandler lastButton;

	Node lastNode;

	public Color buttonSelectColor;

	BuildManager manager;

	public GameObject nodeParent;

	public GameObject inspector;

    public Text elevateCost;

    public Text sellRefund;

    public Button elevateButton;

	public void Start()
	{
		manager = BuildManager.GetInstance();

		ButtonHandler[] handlers = GetComponentsInChildren<ButtonHandler>();

		foreach (ButtonHandler handler in handlers) handler.selection += new ButtonHandler.SelectEvent(setBlueprint);

		Node[] nodes = nodeParent.GetComponentsInChildren<Node>();

		foreach (Node node in nodes) node.selection += new Node.SelectEvent(setNode);
	}

    public void setBlueprint(ButtonHandler button, Blueprint blueprint)
    {
    	if (lastNode != null) {

    		lastNode.deselect();

    		lastNode = null;

    		inspector.SetActive(false);
    	}
    	if (lastButton != null) {

            lastButton.resetColor();

    		if (lastButton == button) {

                lastButton = null;

                manager.setBlueprint(null);

                return;
            }
    	}
	    lastButton = button;
	    
	    button.setColor(buttonSelectColor);
    	
    	manager.setBlueprint(blueprint);
    }

    public void setNode(Node node)
    {
    	if (lastButton != null) {

    		lastButton.resetColor();

	    	lastButton = null;

            manager.setBlueprint(null);
    	}
    	if (lastNode != null) {

    		if (lastNode == node) {

    			inspector.SetActive(false);

    			lastNode = null;

    			return;
			}
			lastNode.deselect();
    	}
    	lastNode = node;
		
        activateInspector(node);
    }

    void activateInspector(Node node)
    {
        if (node.canElevate) {

            elevateCost.text = "$ " + node.getElevateCost();

            elevateButton.interactable = true;
        
        } else {

            elevateCost.text = "Done";

            elevateButton.interactable = false;
        }
        sellRefund.text = "$ " + node.getSellRefund();
        
        inspector.transform.position = node.transform.position;

        inspector.SetActive(true);
    }

    public void upgrade()
    {
        if (lastNode.upgrade()) {

            inspector.SetActive(false);

            lastNode = null;
        }
    }

    public void sell()
    {
        lastNode.sell();

        inspector.SetActive(false);

        lastNode = null;
    }
}
