using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	[SerializeField]
	private GameObject selectionCanvas;
	[SerializeField]
	private GameObject locationPointerPrefab;

	private bool isSelected;

	private NavMeshAgent agent;

	private Collider lastSelected = null;


	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	public void setisSelected(bool isSelected)
	{
		selectionCanvas.SetActive(isSelected);
	}

	private void OnMouseDown()
	{
		GameManager.Instance.DeselectAllSelectedUnits();
		GameManager.Instance.currentSelectedUnit = this.gameObject;
		GameManager.Instance.selectedGroupUnits.Add(this.gameObject);
		setisSelected(true);
	}

	private void MoveToDirection(RaycastHit hit)
	{
		// Find the direction to move in
		Vector3 dir = hit.point - transform.position;

		// Make it so that its only in x and y axis
		dir.y = 0; // No vertical movement
			   // Now move your character in world space 
		agent.destination = new Vector3(hit.point.x, 0, hit.point.z);

		GameObject obj = Instantiate(locationPointerPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
		Destroy(obj, 1);
		// transform.Translate (dir * Time.DeltaTime * speed); // Try this if it doesn't work
	}

	public void MoveUnit(RaycastHit hit)
	{
		MoveToDirection(hit);		
	}

}
