using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class Building : MonoBehaviour
{
	[SerializeField]
	private GameObject selectionCanvas;

	private bool isSelected;

	protected virtual void OnMouseDown()
	{

		BuildingManager.Instance.currentSelectedBuilding= this.gameObject;
		setisSelected(true);

		UnitManager.Instance.DeselectAllSelectedUnits(); //deselctingall units when selecting buildings
	}

	private void Start()
	{
		BuildingManager.Instance.AllBuildings.Add(this);
	}

	public void setisSelected(bool isSelected)
	{
		selectionCanvas.SetActive(isSelected);
	}

	public void upDateBuilding()
	{
		isSelected = (BuildingManager.Instance.currentSelectedBuilding ==this.gameObject) ? true : false;
		setisSelected(isSelected);
	}

}
