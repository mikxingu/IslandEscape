using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
	NavMeshAgent currentAgent;

	Ray lastRay;
	private void Start()
	{
		currentAgent = GetComponent<NavMeshAgent>();
	}

	void Update()
    {
		if (Input.GetMouseButton(0))
		{
			MoveToCursor();
			
		}
		
		if (Input.GetKeyDown(KeyCode.S))
		{
			currentAgent.Stop();
			//targetTransform = null;
		}

		UpdateAnimator();
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

	void UpdateAnimator()
	{
		Vector3 velocity = currentAgent.velocity;
		Vector3 localVelocity = transform.InverseTransformDirection(velocity);
		float speed = localVelocity.z;
		GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
	}
}
