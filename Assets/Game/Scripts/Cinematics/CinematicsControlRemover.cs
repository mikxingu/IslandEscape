using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;

namespace RPG.Cinematics
{
	public class CinematicsControlRemover : MonoBehaviour
	{
		[SerializeField] PlayerController playerController;

		PlayableDirector currentDirector;

		private void Start()
		{
			currentDirector = GetComponent<PlayableDirector>();
			currentDirector.stopped += EnableControls;
			currentDirector.played += DisableControls;
			
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

		void Update()
		{
			/*
			print(currentDirector.time);
			if (currentDirector.time >= 16){
				currentDirector.enabled = false;
			}*/
		
		}
	}
}


