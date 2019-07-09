using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	//the main seleccted unit/buidling in the whole scene one should only be allowed with one click
	public GameObject currentSelectedUnit;

	public List<GameObject> selectedGroupUnits;

	//for group selection
	public bool isGroupSelecting;

	private Vector3 start_box;

	private Vector3 end_box;

	private Rect boundbox;

	private bool unitIsSelected;

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

		selectedGroupUnits = new List<GameObject>();
	}

	private void Update()
	{

		SelecteUnits();
		UpdateGroupUnits();
	}

	private void makeBox()
	{
		//Ensures the bottom left and top right values are correct
		//regardless of how the user boxes units
		float xmin = Mathf.Min(start_box.x, end_box.x);
		float ymin = Mathf.Min(start_box.y, end_box.y);
		float width = Mathf.Max(start_box.x, end_box.x) - xmin;
		float height = Mathf.Max(start_box.y, end_box.y) - ymin;
		boundbox = new Rect(xmin, ymin, width, height);
	}

	private void SelecteUnits()
	{

		if (Input.GetMouseButtonDown(0))
		{
			isGroupSelecting = true;
			start_box = Input.mousePosition;

			if (hit.collider.gameObject.tag != "Selectable")
			{
				DeselectAllSelectedUnits();
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			end_box = Input.mousePosition;
			makeBox();
			GameObject[] selectedUnits = GameObject.FindGameObjectsWithTag("Selectable"); //improve performance by not usng findGameObject with tag
			for (int i = 0; i < selectedUnits.Length; i++)
			{
				Vector3 objectlocation = Camera.main.WorldToScreenPoint(new Vector3(selectedUnits[i].transform.position.x, selectedUnits[i].transform.position.y, selectedUnits[i].transform.position.z));

				//If the object falls inside the box set its state to selected so we can use it later
				if (boundbox.Contains(objectlocation))
				{
					selectedUnits[i].GetComponent<Unit>().setisSelected(true);
					selectedGroupUnits.Add(selectedUnits[i]);
					//TODO use event instead of getComponenet to improve performance
				}
			}

			isGroupSelecting = false;
		}

	}

	void OnGUI()
	{
		//Utils.DrawScreenRectBorder(new Rect(32, 32, 256, 128), 2, Color.green);
		if (isGroupSelecting)
		{
			// Create a rect from both mouse positions
			var rect = Utils.GetScreenRect(start_box, Input.mousePosition);
			Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.15f));
			Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
		}
	}

	private void UpdateGroupUnits()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			if (Input.GetKey(KeyCode.Mouse1))
			{

				for (int i = 0; i < selectedGroupUnits.Count; i++)
				{
				
					Unit unit = selectedGroupUnits[i].GetComponent<Unit>();
					unit.MoveUnit(hit); //avoid getting componenet every frame use events instead
				}
			}
		}
	}

	public void DeselectAllSelectedUnits()
	{
		for (int i = 0; i < selectedGroupUnits.Count; i++)
		{

			selectedGroupUnits[i].GetComponent<Unit>().setisSelected(false); // avoid using get componenet every itiration
		}
		selectedGroupUnits.Clear();
	}

}
