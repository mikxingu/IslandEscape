using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
	public class Fighter : MonoBehaviour
	{
		[SerializeField] float weaponRange = 2f;
		Transform targetTransform;

		CharacterMover characterMover;

		private void Start()
		{
			characterMover = GetComponent<CharacterMover>();
		}

		private void Update()
		{
			bool isInRange = Vector3.Distance(transform.position, targetTransform.position) < weaponRange;

			if (targetTransform == null) return;
			
			// If I have a target, I should agro it.
			if (!isInRange)
			{
				characterMover.MoveTo(targetTransform.position);
			}
			else
			{
				//characterMover.Stop();
			}
		}
		public void Attack(CombatTarget combatTarget)
		{
			targetTransform = combatTarget.transform;
		}
    }

}
