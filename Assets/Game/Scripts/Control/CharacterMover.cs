using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Combat;

namespace RPG.Movement 
{
	public class CharacterMover : MonoBehaviour, IAction
	{
		NavMeshAgent currentAgent;
		Fighter currentFighter;
		ActionScheduler actionScheduler;
		Health currentHealth;

		float defaultMovementSpeed;


		private void Start()
		{
			actionScheduler = GetComponent<ActionScheduler>();
			currentAgent = GetComponent<NavMeshAgent>();
			currentFighter = GetComponent<Fighter>();
			currentHealth = GetComponent<Health>();
			defaultMovementSpeed = currentAgent.speed;
		}


		private void Update()
		{
			currentAgent.enabled = !currentHealth.IsDead();
			currentFighter.enabled = !currentHealth.IsDead();
			UpdateAnimator();
		}


		public void MoveToPoint(Vector3 destination)
		{
		 	currentAgent.stoppingDistance = 0;
			currentAgent.SetDestination(destination);
			actionScheduler.StartAction(this);
		}


		public void MoveToAttack(Vector3 destination)
		{
			currentAgent.stoppingDistance = currentFighter.defaultWeaponRange;
			currentAgent.SetDestination(destination);
			actionScheduler.StartAction(this);
		}

		public void ChangeMovementSpeed (float speed)
		{
			currentAgent.speed += defaultMovementSpeed * speed;
		}

		public void CancelAction()
		{

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
