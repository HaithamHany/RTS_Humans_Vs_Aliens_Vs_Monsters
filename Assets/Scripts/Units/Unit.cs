using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Unit : MonoBehaviour
{
	[SerializeField]
	private GameObject selectionCanvas;

	private bool isSelected;

	public NavMeshAgent agent;

	private Collider lastSelected = null;

	//unit general properties
	[SerializeField]
	protected float Speed;

	[SerializeField]
	protected float Health;

	[SerializeField]
	protected float Damage;

	[SerializeField]
	protected bool isDead;


	protected virtual void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		UnitManager.Instance.AllUnits.Add(this.gameObject.GetComponent<Unit>());
	}

	public void setisSelected(bool isSelected)
	{
		selectionCanvas.SetActive(isSelected);
	}

	protected virtual void OnMouseDown()
	{
		UnitManager.Instance.DeselectAllSelectedUnits();
		UnitManager.Instance.currentSelectedUnit = this.gameObject;
		UnitManager.Instance.selectedGroupUnits.Add(this.gameObject);
		setisSelected(true);
	}

	private void MoveToDirection(RaycastHit hit)
	{
		// Find the direction to move in
		Vector3 dir = hit.point - transform.position;

		// Make it so that its only in x and y axis
		dir.y = 0; // No vertical movement

		agent.destination = new Vector3(hit.point.x, 0, hit.point.z);
		// transform.Translate (dir * Time.DeltaTime * speed); // Try this if it doesn't work
	}

	public void MoveUnit(RaycastHit hit)
	{
		MoveToDirection(hit);

	}

	private void setData()
	{
		agent.speed = Speed;
	}

	public virtual void upDateUnit()
	{
		setData();
	}

}
