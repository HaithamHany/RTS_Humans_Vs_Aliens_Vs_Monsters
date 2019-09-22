using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshObstacle))]
public abstract class Building : MonoBehaviour
{
	[SerializeField]
	private GameObject selectionCanvas;

	[SerializeField]
	private GameObject item;

	private bool isSelected;

	//properties
	protected string BuildingName;

	protected int ItemsNumber;

	protected float Health;

	protected bool isDestroyed;

	protected Material buildingMat;

	//items info
	protected string buildingName;

	protected int numberOfItems;

	protected List<GameObject> items;

	[SerializeField]
	private GameObject itemsMenuParent;


	protected virtual void OnMouseDown()
	{

		BuildingManager.Instance.currentSelectedBuilding= this.gameObject;
		setisSelected(true);		
		UnitManager.Instance.DeselectAllSelectedUnits(); //deselctingall units when selecting buildings			
		ShowBuildingItems();
	}

	protected virtual void Start()
	{
		BuildingManager.Instance.AllBuildings.Add(this);
		setData();
		items = new List<GameObject>();
	}

	protected virtual void ShowBuildingItems()
	{
		if(isSelected)
		{
			for (int i = 0; i < numberOfItems; i++)
			{
				GameObject item = Instantiate(this.item);
				item.transform.SetParent(itemsMenuParent.transform, false);
				items.Add(item);
			}
		}
				
	}


	protected virtual void setData()
	{

	}

	public void setisSelected(bool _isSelected)
	{
		selectionCanvas.SetActive(_isSelected);
		isSelected = _isSelected;
	}

	private void removeItems()
	{
		if(!isSelected)
		{
			foreach (var item in items)
			{
				Destroy(item); //temporary. should be using object pooling
			}

			items.Clear();
		}
	}

	public void UpDateBuilding()
	{
		isSelected = (BuildingManager.Instance.currentSelectedBuilding == this.gameObject) ? true : false;
		setisSelected(isSelected);
		removeItems();
	}

	

}
