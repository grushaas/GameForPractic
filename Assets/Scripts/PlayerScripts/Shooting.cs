using UnityEngine;

public class Shooting : MonoBehaviour
{
    private int ammo;
    [SerializeField]
    public int countAmmo;
    [SerializeField]
    private GameObject bullet;
    public Transform bulletSpawnPoint;

    private UIStats uiStats;

    private Animator anim;

    private void Start()
    {
        countAmmo = 20;

        anim = GetComponent<Animator>();
        uiStats = FindFirstObjectByType<UIStats>();
        uiStats.UpdateAmmoLabel(countAmmo);
        ammo = countAmmo;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (countAmmo > 0)
            {
                anim.SetBool("isShooting", true);
                Debug.Log(anim.GetBool("isShooting"));
                Shoot();
            }
            else
            {
                StartReload();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        countAmmo--;
        if(uiStats != null)
        {
            uiStats.UpdateAmmoLabel(countAmmo);
        }
        anim.SetBool("isShooting", false);
    }

    private void StartReload()
    {
        anim.SetBool("isReload", true);
        Invoke("FinishReload", 1f);
    }

    private void FinishReload()
    {
        countAmmo = ammo;
        if (uiStats != null)
        {
            uiStats.UpdateAmmoLabel(countAmmo);
        }
        anim.SetBool("isReload", false);
    }
}