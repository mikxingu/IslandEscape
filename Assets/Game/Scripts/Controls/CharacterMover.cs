using RPG.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement 
{
	public class CharacterMover : MonoBehaviour
	{
		NavMeshAgent currentAgent;

		private void Start()
		{
			currentAgent = GetComponent<NavMeshAgent>();
		}

		private void Update()
		{
			UpdateAnimator();
		}

		public void MoveToPoint(Vector3 destination)
		{
			currentAgent.SetDestination(destination);
		}

		public void Stop()
		{
			currentAgent.Stop();
			print("Stopping");
		}

		void UpdateAnimator()
		{
			Vector3 velocity = currentAgent.velocity;
			Vector3 localVelocity = transform.InverseTransformDirection(velocity);
			float speed = localVelocity.z;
			GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
		}
	}
}
