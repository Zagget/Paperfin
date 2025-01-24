using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int minHealth;
    [SerializeField] int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < minHealth)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
