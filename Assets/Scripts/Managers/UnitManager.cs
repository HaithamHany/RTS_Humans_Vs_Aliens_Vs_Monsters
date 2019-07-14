using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
	public static UnitManager Instance;

	//the main seleccted unit/buidling in the whole scene one should only be allowed with one click
	public GameObject currentSelectedUnit;

	public List<GameObject> selectedGroupUnits;

	//for group selection
	public bool isGroupSelecting;

	public List<Unit> AllUnits;

	private Vector3 start_box;

	private Vector3 end_box;

	public Rect boundbox;

	private bool unitIsSelected;

	public  bool isClicked;

	[SerializeField]
	private GameObject locationPointerPrefab;

	private GameObject locationPointer;

	private float locationPointerTimer;

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
		AllUnits = new List<Unit>();
		createLocationPointer();
	}

	public void UpdateUnits(RaycastHit hit)
	{

		SelecteUnits(hit);
		UpdateGroupUnits(hit);
		UpdateIndividualUnits();
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

	private void SelecteUnits(RaycastHit hit)
	{

		if (Input.GetMouseButtonDown(0))
		{
			isGroupSelecting = true;
			start_box = Input.mousePosition;

			if (hit.collider.gameObject.tag != "Selectable")
			{
				DeselectAllSelectedUnits();
				activatePointer(false);
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			end_box = Input.mousePosition;
			makeBox();
			//GameObject[] selectedUnits = GameObject.FindGameObjectsWithTag("Selectable"); //improve performance by not usng findGameObject with tag
			for (int i = 0; i < AllUnits.Count; i++)
			{			
				Vector3 objectlocation = Camera.main.WorldToScreenPoint(new Vector3(AllUnits[i].transform.position.x, AllUnits[i].transform.position.y, AllUnits[i].transform.position.z));

				//If the object falls inside the box set its state to selected so we can use it later
				if (boundbox.Contains(objectlocation))
				{
					AllUnits[i].setisSelected(true);
					selectedGroupUnits.Add(AllUnits[i].gameObject);
					//AllUnits[i].agent.ResetPath();

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

	private void UpdateGroupUnits(RaycastHit hit)
	{
		float stopingDistance = 0;
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			for (int i = 0; i < selectedGroupUnits.Count; i++)
			{
				Unit unit = selectedGroupUnits[i].GetComponent<Unit>();
				unit.agent.stoppingDistance = 10;
			}

			for (int i = 0; i < selectedGroupUnits.Count; i++)
			{
				stopingDistance += 5;
				Unit unit = selectedGroupUnits[i].GetComponent<Unit>();
				unit.agent.ResetPath();
				unit.MoveUnit(hit); //avoid getting componenet every frame use events instead
				unit.agent.stoppingDistance += stopingDistance;
				//GameObject obj = Instantiate(locationPointerPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
				//Destroy(obj, 1);
				activatePointer(hit);
			}
		}

		checkPointerTimer(2.5f);
	}

	private void UpdateIndividualUnits()
	{
		for (int i = 0; i < AllUnits.Count; i++)
		{
			AllUnits[i].upDateUnit();
		}
	}

	public void DeselectAllSelectedUnits()
	{
		for (int i = 0; i < selectedGroupUnits.Count; i++)
		{
			Unit unit = selectedGroupUnits[i].GetComponent<Unit>();
			unit.setisSelected(false); // avoid using get componenet every itiration
			//unit.agent.stoppingDistance = 10;
		}
		selectedGroupUnits.Clear();
	}

	private void createLocationPointer()
	{
		locationPointer = Instantiate(locationPointerPrefab);
		locationPointer.SetActive(false);
		
	}

	private void activatePointer(RaycastHit hit)
	{
		isClicked = false;
		locationPointer.SetActive(true);
		locationPointer.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
		isClicked = true;
	}

	private void activatePointer(bool status)
	{
		locationPointer.SetActive(status);
	}

	private void checkPointerTimer(float timeLimit)
	{
		if(isClicked)
		{
			locationPointerTimer += 0.1f;
		}

		if (locationPointerTimer >= timeLimit)
		{
			activatePointer(false);
			locationPointerTimer = 0;
			isClicked = false;
		}
	}

	public bool IsWithinSelectionBounds(GameObject gameObject)
	{
		if (!isGroupSelecting)
			return false;

		var camera = Camera.main;
		var viewportBounds =
		    Utils.GetViewportBounds(camera, start_box, end_box);

		return viewportBounds.Contains(
		    camera.WorldToViewportPoint(gameObject.transform.position));
	}

}
