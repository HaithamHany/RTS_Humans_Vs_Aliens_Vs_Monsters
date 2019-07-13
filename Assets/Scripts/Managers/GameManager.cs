using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitManager))]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	private UnitManager unitManager;

	Ray ray;

	RaycastHit hit;

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
	}

	private void Update()
	{
		UpdateUnitManager();
	}

	private void UpdateUnitManager()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			unitManager.UpdateUnits(hit);
		}
	}

}
