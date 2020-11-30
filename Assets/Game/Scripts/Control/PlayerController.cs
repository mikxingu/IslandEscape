using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
	public class PlayerController : MonoBehaviour
	{
		CharacterMover playerMover;

		Fighter playerFighter;

		Health playerHealth;

		private void Start()
		{
			playerHealth = GetComponent<Health>();
			playerMover = GetComponent<CharacterMover>();
			playerFighter = GetComponent<Fighter>();
		}
		void Update()
		{
			if (playerHealth.IsDead()) return;
			HandleInput();
		}

		void HandleInput()
		{
			// Right click for combat/interaction
			InteractRightClick();

			// Left Click For movement/selection
			InteractLeftClick();

			// Middle Mouse To Control Camera
			//ControlCamera();
		}

		

		bool InteractLeftClick()
		{
			RaycastHit hit;
			bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
			if (hasHit)
			{
				if (Input.GetMouseButton(0))
				{
					playerMover.MoveToPoint(hit.point);
					playerFighter.CancelAction();
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
					
					if (target == null) continue;

					GameObject targetGameObject = target.gameObject;

					if (Input.GetMouseButtonDown(1))
					{
						if (target != null)
						{
							playerFighter.Attack(target.gameObject);
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

