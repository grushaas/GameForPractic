using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float lightDuration = 5f;
    private float timeInDay;
    private bool isDayTime = true;
    private int currentZombieCount;
    private int currentWave = 1;

    private float zombieSpeed = 1f;
    private float zombieHealth = 100f;
    private float zombieDamage = 15f;

    void Start()
    {
        currentZombieCount = 20;
        timeInDay = 0f;
        
        
        //Debug.Log("posStart: " + player.transform.position);
    }

    private void Update()
    {
        Generation gen = GetComponent<Generation>();
        //Debug.Log("posUpdate: " + player.transform.position);
        if (isDayTime)
        {
            timeInDay += Time.deltaTime;

            if (timeInDay >= lightDuration)
            {
                
                gen.ChangeNightFloor();
                isDayTime = false;
                timeInDay = 0f;
                Debug.Log("Nighttime started");
                SpawnZombies(currentZombieCount);
            }
        }
        else
        {
            if (!IsZombiesAlive())
            {
                gen.ChangeDayFloor();
                isDayTime = true;
                currentZombieCount += 5;
                currentWave++;

                IncreaseZombieStats();

                Debug.Log("Daytime started");
            }
        }
    }

    private void SpawnZombies(int count)
    {
        PlayerController playContr = FindFirstObjectByType<PlayerController>();
        if(playContr == null)
        {
            Debug.Log("--NULL--");
            return;
        }
        //Debug.Log("posSpawn: " + player.transform.position);
        for (int i = 0; i < count; i++)
        {
            GameObject zombie = Instantiate(zombiePrefab, playContr.getPosition() + Random.insideUnitCircle * 10f, Quaternion.identity);
            EnemyController enemyController = zombie.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.SetStats(zombieSpeed, zombieHealth, zombieDamage);
            }
            else
            {
                Debug.LogWarning("ZombieController not found on the zombie prefab");
            }
        }
    }

    private void IncreaseZombieStats()
    {
        if(currentWave <= 10)
        {
            zombieSpeed += 0.1f;
        }

        zombieHealth += 0.2f;

        if(currentWave % 5 == 0)
        {
            zombieDamage += 1f;
        }
    }

    private bool IsZombiesAlive()
    {
        return GameObject.FindGameObjectsWithTag("Zombie").Length > 0;
    }
}