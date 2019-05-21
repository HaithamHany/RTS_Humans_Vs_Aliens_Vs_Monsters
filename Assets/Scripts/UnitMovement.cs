using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    [SerializeField] GameObject selectionCanvas;
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
        if (Physics.Raycast(ray, out hit))
        {
            // Find the direction to move in
            Vector3 dir = hit.point - transform.position;

            // Make it so that its only in x and y axis
            dir.y = 0; // No vertical movement
            Debug.Log(dir);
            // Now move your character in world space 
            agent.destination = dir;

            // transform.Translate (dir * Time.DeltaTime * speed); // Try this if it doesn't work
        }
    }

    private void OnMouseDown()
    {
        isSelected = !isSelected;
        selectionCanvas.SetActive(isSelected);
    }
}
