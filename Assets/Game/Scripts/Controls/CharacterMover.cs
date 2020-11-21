using RPG.Combat;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement 
{
	public class CharacterMover : MonoBehaviour, IAction
	{
		NavMeshAgent currentAgent;
		Fighter currentFighter;
		ActionScheduler actionScheduler;

		private void Start()
		{
			actionScheduler = GetComponent<ActionScheduler>();
			currentAgent = GetComponent<NavMeshAgent>();
			currentFighter = GetComponent<Fighter>();
		}

		private void Update()
		{
			UpdateAnimator();
		}

		public void MoveToPoint(Vector3 destination)
		{
			if (currentFighter.weaponRange != 0)
			{
				currentAgent.stoppingDistance = currentFighter.weaponRange;
			}
			
			currentAgent.SetDestination(destination);
			actionScheduler.StartAction(this);
		}

		public void CancelAction()
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
