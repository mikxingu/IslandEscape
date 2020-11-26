using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;


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
		[SerializeField] PatrolPath patrolPath;
		[SerializeField] float waypointTolerance = 1f;
		[SerializeField] float movementSpeed;

		CombatState currentState = CombatState.guarding;

		CharacterMover aiMover;
		Fighter aiFighter;
		Health health;

		Vector3 guardPosition;
		Quaternion guardRotation;

		float timeLastSeenPlayer = 0;
		float timeInWayPoint = 0f;
		[SerializeField] float suspicionTime = 3f;
		[SerializeField] float wayPointTime = 2f;

		int currentWaypointIndex = 0;

		private void Start()
		{
			aiFighter = GetComponent<Fighter>();
			aiMover = GetComponent<CharacterMover>();
			health = GetComponent<Health>();
			guardPosition = transform.position;
			guardRotation = transform.rotation;
		}

		private void Update()
		{
			if (health.IsDead()) return;

			GameObject player = GameObject.FindWithTag("Player");
			bool isInChaseRadius = Vector3.Distance(transform.position, player.transform.position) < combatRadius;

			if (isInChaseRadius && player.GetComponent<CapsuleCollider>().enabled)
			{
				timeLastSeenPlayer = 0;
				print(gameObject.name + " will chase the player");
				currentState = CombatState.fighting;
				aiFighter.Attack(player);
				aiMover.SetDefaultMovementSpeed();

			}
			else if (timeLastSeenPlayer < suspicionTime)
			{
				currentState = CombatState.suspicious;
				aiFighter.CancelAction();
			}

			
			else
			{
				Vector3 nextPosition = guardPosition;

				if (patrolPath != null)
				{
					if (AtWaypoint())
					{
						timeInWayPoint = 0f;
						currentState = CombatState.patrolling;
						CycleWaypoint();
						aiMover.SetMovementSpeed(movementSpeed);
					}
					if (timeInWayPoint >= wayPointTime)
					{
						nextPosition = GetCurrentWayPoint();
						aiMover.MoveToPoint(nextPosition);
					}
					
					timeInWayPoint += Time.deltaTime;
				}
				else
				{
					aiMover.SetDefaultMovementSpeed();
					currentState = CombatState.guarding;
					aiFighter.CancelAction();
					aiMover.MoveToPoint(guardPosition);
					if (IsAtGuardPosition())
					{
						aiMover.transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, 3f * Time.deltaTime);
					}
					
				}
				

				
			}

			timeLastSeenPlayer += Time.deltaTime;

		}

		bool IsAtGuardPosition()
		{
			if (Vector3.Distance(transform.position, guardPosition) < waypointTolerance)
			{
				return true;
			}
			
			return false;
		}

		private void CycleWaypoint()
		{
			currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
		}

		private bool AtWaypoint()
		{
			float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
			return distanceToWaypoint < waypointTolerance;
		}

		private Vector3 GetCurrentWayPoint()
		{
			return patrolPath.GetWaypoint(currentWaypointIndex);
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


