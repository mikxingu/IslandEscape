using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;

namespace RPG.Cinematics
{
	public class CinematicsControlRemover : MonoBehaviour
	{
		[SerializeField] PlayerController playerController;
		private void Start()
		{
			GetComponent<PlayableDirector>().stopped += EnableControls;
			GetComponent<PlayableDirector>().played += DisableControls;
			
		}
		void EnableControls(PlayableDirector playableDirector)
		{
			print("Enabled Controls");
			playerController.enabled = true;
			
		}

		void DisableControls(PlayableDirector playableDirector)
		{
			print("Disabled Controls");
			playerController.enabled = false;
		}
	}
}


