using UnityEngine;
using RPG.Combat;


/* == TODO ==
 * Implement Line of Sight and Combat Radius:
 * Combat Radius: The radius of enemy awareness. If you attack one particular enemy, all enemies 
 * nearby will notice and will automatically follow will. The Condition will be like 
 * if one particular enemy has the aggro on you and the other enemies are in this enemy combat range, they will follow will even if you're not on their combat radius.
 * 
 * Line of Sight: The enemies only look forward, so, they will only target you if you cross their line of sight.
 * This approach will give players the potential to sneak behind enemies to avoid combat, especially on situations where they are outnumbered.
 * 
 */

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float combatRadius = 5f;

		Fighter aiFighter;

		private void Start()
		{
			aiFighter = GetComponent<Fighter>();
		}

		private void Update()
		{
			GameObject player = GameObject.FindWithTag("Player");
			bool isInChaseRadius = Vector3.Distance(transform.position, player.transform.position) < combatRadius;

			if (isInChaseRadius)
			{
				aiFighter.Attack(player);
				print(gameObject.name + " will chase the player");
			}
			else
			{
				aiFighter.CancelAction();
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(transform.position, combatRadius);
		}


		/* THIS WILL BE USED AS LINE OF SIGHT IN THE FUTURE 
		void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawRay(transform.position, (Vector3.forward * chaseRadius));
		}*/
	}
}


