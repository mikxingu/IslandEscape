using UnityEngine;

namespace RPG.Combat
{

    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

		bool isDead = false;

		public bool IsDead()
		{
			return isDead;
		}
		public void TakeDamage(float amount)
		{
            
            health = Mathf.Max(health - amount, 0);
            print(health);
            if (health == 0)
			{
				Die();
				//Destroy(this.gameObject); //THIS WILL BE ENABLED AFTER
			}

		}

		private void Die()
		{
			
			if (isDead) return;

			isDead = true;
			GetComponent<Animator>().SetTrigger("die");
			GetComponent<CapsuleCollider>().enabled = false;
		}
	}

}
