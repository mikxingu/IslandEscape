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
		}

		private void AttackBehaviour()
		{
			FaceTarget();
			if (lastAttackTime > attackCooldown)
			{
				TriggerAttack();
				lastAttackTime = 0f;
			}

		}

		private void TriggerAttack()
		{
			GetComponent<Animator>().ResetTrigger("stopattack");
			GetComponent<Animator>().SetTrigger("attack");
		}

		public void Attack(CombatTarget combatTarget)
		{
			actionScheduler.StartAction(this);
			target = combatTarget.GetComponent<Health>();
		}

		public void CancelAction()
		{
			StopAttack();
			target = null;

		}

		private void StopAttack()
		{
			GetComponent<Animator>().ResetTrigger("attack");
			GetComponent<Animator>().SetTrigger("stopattack");
		}

		void FaceTarget()
		{
			Vector3 direction = (target.transform.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
		}

		//Animation Event
		void Hit()
		{
			if (target != null)
			{
				target.TakeDamage(weaponDamage);
			}
		}
    }
}
