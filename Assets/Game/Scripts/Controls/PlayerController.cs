using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
	public class PlayerController : MonoBehaviour
	{
		CharacterMover characterMover;
		private void Start()
		{
			characterMover = GetComponent<CharacterMover>();
		}
		void Update()
		{
			HandleInput();
		}

		void HandleInput()
		{
			// Left Click For movement
			if (Input.GetMouseButton(0))
			{

				MoveToCursor();

			}

			// "S" Key to Stop
			if (Input.GetKeyDown(KeyCode.S))
			{
				characterMover.Stop();
				//targetTransform = null;
			}
		}
		void MoveToCursor()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;
			RaycastHit hit;
			bool hasHit = Physics.Raycast(ray, out hit);
			if (hasHit)
			{
				characterMover.MoveTo(hit.point);
			}
		}
	}
}

