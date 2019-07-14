using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
	public static BuildingManager Instance;

	public GameObject currentSelectedBuilding;

	public List<Building> AllBuildings;

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

		AllBuildings = new List<Building>();
	}

	public void UpdateAllBuildings()
	{
		for (int i = 0; i < AllBuildings.Count; i++)
		{
			AllBuildings[i].upDateBuilding();
		}
	}
}
