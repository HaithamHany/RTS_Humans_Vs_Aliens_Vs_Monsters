using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	[SerializeField] GameObject selectionCanvas;
	[SerializeField] GameObject locationPointerPrefab;
	private bool isSelected = false;
	private NavMeshAgent agent;

	Ray ray;
	RaycastHit hit;

	private void Start()
	{
		//ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			if (Input.GetKey(KeyCode.Mouse0) && GameManager.instance.selectedObject == this.gameObject)
			{
				// Find the direction to move in
				Vector3 dir = hit.point - transform.position;

				// Make it so that its only in x and y axis
				dir.y = 0; // No vertical movement
				Debug.Log(dir);
				// Now move your character in world space 
				agent.destination = new Vector3(hit.point.x,0, hit.point.z);

				GameObject obj = Instantiate(locationPointerPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
				Destroy(obj, 1);
				// transform.Translate (dir * Time.DeltaTime * speed); // Try this if it doesn't work
			}
		}

		isSelected = (GameManager.instance.selectedObject == this.gameObject) ? true : false;
		selectionCanvas.SetActive(isSelected);
	}

	private void OnMouseDown()
	{
		GameManager.instance.selectedObject = this.gameObject;
		
	}
}
