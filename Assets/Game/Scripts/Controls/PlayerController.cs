using RPG.Movement;
using RPG.Combat;
using RPG.Core;
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
			// Right click for combat/interaction
			InteractRightClick();

			// Left Click For movement/selection
			InteractLeftClick();

			// Middle Mouse To Control Camera
			ControlCamera();
		}

		

		bool InteractLeftClick()
		{
			RaycastHit hit;
			bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
			if (hasHit)
			{
				if (Input.GetMouseButton(0))
				{
					characterMover.MoveToPoint(hit.point);
					characterFighter.CancelAction();
				}
				return true;
			}
			return false;
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
			
		private static Ray GetMouseRay()
		{
			return Camera.main.ScreenPointToRay(Input.mousePosition);
		}

		void ControlCamera()
		{
			CameraController cam = Camera.main.GetComponent<CameraController>();
			cam.MoveCamera();

		}
	}
}

