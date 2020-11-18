using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Transform targetTransform = null;

    NavMeshAgent currentAgent;
	private void Start()
	{
		currentAgent = GetComponent<NavMeshAgent>();
	}

	void Update()
    {
        if (targetTransform != null)
		{
			currentAgent.SetDestination(targetTransform.position);
		}
		if (Input.GetKeyDown(KeyCode.K))
		{
			targetTransform = null;
		}
    }
}
