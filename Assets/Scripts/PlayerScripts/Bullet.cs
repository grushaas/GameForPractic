using Unity.Hierarchy;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeDestroy = 3f;
    public float speed = 3f;
    public float damage = 10f;

    private UIStats uiStats;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        uiStats = FindFirstObjectByType<UIStats>();
        uiStats.UpdateStrengthLabel(damage);

        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);

        rb.linearVelocity = transform.right * speed;
        Invoke("DestroyBullet", timeDestroy);
    }

    public void newDamage(float damage)
    {
        this.damage += damage;

        if(uiStats != null)
        {
            uiStats.UpdateStrengthLabel(this.damage);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if(damageable != null )
        {
            damageable.TakeDamage(damage);
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
