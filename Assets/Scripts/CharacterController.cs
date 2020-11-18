using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    //[SerializeField] Transform targetTransform = null;

    NavMeshAgent currentAgent;

	Ray lastRay;
	private void Start()
	{
		currentAgent = GetComponent<NavMeshAgent>();
	}

	void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			MoveToCursor();
			
		}
		

		if (Input.GetKeyDown(KeyCode.S))
		{
			currentAgent.Stop();
			//targetTransform = null;
		}
    }

	void MoveToCursor()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;
		RaycastHit hit;
		bool hasHit = Physics.Raycast(ray, out hit);
		if (hasHit)
		{
			currentAgent.SetDestination(hit.point);
		}
	}
}
