using UnityEngine;
using UnityEngine.Playables;
using RPG.Movement;

namespace RPG.Cinematics
{
	
    public class CinematicTrigger : MonoBehaviour
    {
		bool hasPlayed = false;
		CharacterMover playerMover;
		private void OnTriggerEnter(Collider other)
		{
			if (!hasPlayed && other.gameObject.tag == "Player")
			{
				playerMover = other.GetComponent<CharacterMover>();
				playerMover.MoveToPoint(new Vector3(-18, 0, 15));

				GetComponent<PlayableDirector>().enabled = true;
				GetComponent<PlayableDirector>().Play();
				hasPlayed = true;
			}
		}
	}

}
