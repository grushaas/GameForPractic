using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float health = 100f;

    private PlayerController playContr;

    private void Start()
    {
        playContr = FindFirstObjectByType<PlayerController>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (playContr != null)
        {
            playContr.GainExperience(10);
        }
        Destroy(gameObject);
    }
}
