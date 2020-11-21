using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
	public class Fighter : MonoBehaviour, IAction
	{
		public float weaponRange = 2f;
		
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
			if (targetTransform == null) return;
			bool isInRange = Vector3.Distance(transform.position, targetTransform.position) < weaponRange;
			
			if (!isInRange)
			{
				characterMover.MoveToPoint(targetTransform.position);
				
			}
			FaceTarget();
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
    }

}
