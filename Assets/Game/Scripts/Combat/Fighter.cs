using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
	public class Fighter : MonoBehaviour
	{
		[SerializeField] [Range (1f, 10f)] float weaponRange;
		
		[SerializeField] Transform targetTransform;

		CharacterMover characterMover;

		private void Start()
		{
			characterMover = GetComponent<CharacterMover>();
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
			targetTransform = combatTarget.transform;
		}

		public void RemoveTarget()
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
