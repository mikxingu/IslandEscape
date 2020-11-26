using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
	public class Fighter : MonoBehaviour, IAction
	{
		public float defaultWeaponRange = 2f;
		public float weaponDamage = 5f;
		public float attackCooldown = 1f;

		float lastAttackTime = Mathf.Infinity;
		
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

			if (target.IsDead())
			{
				target = null;
				return;
			}

			bool isInRangeToAttack = Vector3.Distance(transform.position, target.transform.position) < defaultWeaponRange;
			
			if (!isInRangeToAttack)
			{
				characterMover.MoveToAttack(target.transform.position);
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
				TriggerAttackAnimation();
				lastAttackTime = 0f;
			}
		}

		public void Attack(GameObject combatTarget)
		{
			actionScheduler.StartAction(this);
			target = combatTarget.GetComponent<Health>();
		}

		public void CancelAction()
		{
			StopAttackAnimation();
			target = null;
		}

		

		//ANIMATION EVENTS
		void Hit()
		{
			if (target != null)
			{
				target.TakeDamage(weaponDamage);
			}
		}

		void FaceTarget()
		{
			Vector3 direction = (target.transform.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
		}


		private void TriggerAttackAnimation()
		{
			GetComponent<Animator>().ResetTrigger("stopattack");
			GetComponent<Animator>().SetTrigger("attack");
		}

		private void StopAttackAnimation()
		{
			GetComponent<Animator>().ResetTrigger("attack");
			GetComponent<Animator>().SetTrigger("stopattack");
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(this.transform.position, defaultWeaponRange);
		}
	}
}
