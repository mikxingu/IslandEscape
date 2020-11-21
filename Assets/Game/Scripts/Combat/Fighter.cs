using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
	public class Fighter : MonoBehaviour, IAction
	{
		public float weaponRange = 2f;
		public float attackCooldown = 1f;

		float lastAttackTime;
		
		[SerializeField] Transform targetTransform;

		CharacterMover characterMover;

		ActionScheduler actionScheduler;

		

		private void Start()
		{
			characterMover = GetComponent<CharacterMover>();
			actionScheduler = GetComponent<ActionScheduler>();
		}

		private void Update()
		{
			lastAttackTime += Time.deltaTime;

			if (targetTransform == null) return;
			bool isInRange = Vector3.Distance(transform.position, targetTransform.position) < weaponRange;
			
			if (!isInRange)
			{
				characterMover.MoveToPoint(targetTransform.position);
				
			}
			else
			{
				characterMover.CancelAction();
				AttackBehaviour();

			}
			FaceTarget();
		}

		private void AttackBehaviour()
		{
			if (lastAttackTime > attackCooldown)
			{
				GetComponent<Animator>().SetTrigger("attack");
				lastAttackTime = 0f;
			}
			
		}

		public void Attack(CombatTarget combatTarget)
		{
			actionScheduler.StartAction(this);
			targetTransform = combatTarget.transform;
		}

		public void CancelAction()
		{
			targetTransform = null;

		}

		void FaceTarget()
		{
			Vector3 direction = (targetTransform.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
		}

		//Animation Event
		void Hit()
		{

		}
    }

}
