using RPG.Movement;
using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
	public class PlayerController : MonoBehaviour
	{
		CharacterMover characterMover;
		Fighter characterFighter;
		private void Start()
		{
			characterMover = GetComponent<CharacterMover>();
			characterFighter = GetComponent<Fighter>();
		}
		void Update()
		{
			HandleInput();
		}

		void HandleInput()
		{
			// Left Click For movement
			InteractLeftClick();

			// Right click for combat
			InteractRightClick();

			// "S" Key to Stop
			if (Input.GetKeyDown(KeyCode.S))
			{
				characterMover.Stop();
				//targetTransform = null;
			}
		}

		

		void InteractLeftClick()
		{
			if (Input.GetMouseButton(0))
			{
				MoveToCursor();
			}
		}
		void InteractRightClick()
		{
				RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
				foreach (RaycastHit hit in hits)
				{
					CombatTarget target = hit.transform.GetComponent<CombatTarget>();

					if (Input.GetMouseButtonDown(1))
					{
						if (target != null)
						{
							characterFighter.Attack(target);
						}
					}
				}
		}

		void MoveToCursor()
		{
			RaycastHit hit;
			bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
			if (hasHit)
			{
				characterMover.MoveTo(hit.point);
			}
		}

		private static Ray GetMouseRay()
		{
			return Camera.main.ScreenPointToRay(Input.mousePosition);
		}
	}
}

