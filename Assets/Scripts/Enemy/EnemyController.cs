using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stat")]
    public float speed = 1f;
    public float damage = 15;
    public float health = 100;

    [Header("Settings attack")]
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    private PlayerController playContr;
    private Animator anim;

    private void Start()
    {
        playContr = FindFirstObjectByType<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playContr != null && Time.timeScale != 0)
        {
            Vector2 direction = (playContr.getPosition() - (Vector2)transform.position).normalized;

            float distance = Vector2.Distance(transform.position, playContr.getPosition());
            if(distance <= attackRange)
            {
                if(Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack();
                }
            }
            else
            {   
                transform.position = Vector2.MoveTowards(transform.position, playContr.getPosition(), speed * Time.deltaTime);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                anim.SetBool("isWalking", direction.magnitude > 0);
            }
        }
    }

    private void Attack()
    {
        anim.SetTrigger("isAttacking");
        playContr.TakeDamage(damage);
        lastAttackTime = Time.time;
    }

    public void SetStats(float newSpeed, float newHealth, float newDamage)
    {
        speed = newSpeed;
        health = newHealth;
        damage = newDamage;
    }
}
