using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitManager))]
[RequireComponent(typeof(BuildingManager))]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	private UnitManager unitManager;

	private BuildingManager buildingManager;

	private Ray ray;

	private RaycastHit hit;

	public List<GameObject> currentSelectedObjects;  //make everyObject register here first

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
		}

		unitManager = GetComponent<UnitManager>();
		buildingManager = GetComponent<BuildingManager>();
	}

	private void Update()
	{
		updateALL();
	}

	private void UpdateUnitManager()
	{
		unitManager.UpdateUnits(hit);
	}

	private void UpdateBuildingManager()
	{
		buildingManager.UpdateAllBuildings();
	}

	private void updateALL()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			UpdateUnitManager();
			UpdateBuildingManager();
		}
	}

	public void ClearSelection(List<GameObject> selectedList)
	{
		currentSelectedObjects.Clear();
	}
}
