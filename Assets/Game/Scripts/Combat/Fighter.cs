using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
	public class Fighter : MonoBehaviour, IAction
	{
		public float weaponRange = 2f;
		public float weaponDamage = 5f;
		public float attackCooldown = 1f;

		float lastAttackTime;
		
		[SerializeField] Health target;

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

			if (target == null) return;

			if (target.IsDead()) return;

			bool isInRange = Vector3.Distance(transform.position, target.transform.position) < weaponRange;
			
			if (!isInRange)
			{
				characterMover.MoveToPoint(target.transform.position);
				
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
			transform.LookAt(target.transform.position);
			if (lastAttackTime > attackCooldown)
			{
				GetComponent<Animator>().SetTrigger("attack");
				lastAttackTime = 0f;
			}
			
		}

		public void Attack(CombatTarget combatTarget)
		{
			actionScheduler.StartAction(this);
			target = combatTarget.GetComponent<Health>();
		}

		public void CancelAction()
		{
			GetComponent<Animator>().SetTrigger("stopattack");
			target = null;

		}

		//void FaceTarget()
		//{
			//Vector3 direction = (target.transform.position - transform.position).normalized;
			//Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
			//transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	//	}

		//Animation Event
		void Hit()
		{
			print("Do Damage");
			target.TakeDamage(weaponDamage);
		}
    }
}
