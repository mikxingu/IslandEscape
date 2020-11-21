using UnityEngine;

namespace RPG.Combat
{

    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;

        public void TakeDamage(float amount)
		{
            health = Mathf.Max(health - amount, 0);
            print(health);
            if (health <= 0)
			{
                Destroy(this.gameObject);
			}

		}
    }

}
