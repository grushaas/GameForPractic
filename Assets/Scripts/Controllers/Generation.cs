using System.Collections;
using UnityEngine;

public class Generation : MonoBehaviour
{
    [Header("Generate floor")]
    [SerializeField]
    private GameObject[] floors;
    [SerializeField]
    private int width = 100; 
    [SerializeField]
    public int height = 100; 

    private bool isGenerated = false;
    private GameObject[,] spawnedFloors;
    private SkillUI skillUI;

    [Header("Player")]
    public GameObject playerPrefab;

    private void Awake()
    {
        spawnedFloors = new GameObject[width, height];
        skillUI = GetComponent<SkillUI>();
        skillUI.CloseSkillInterface();
        SpawnFloor();
    }
    
    private void SpawnFloor()
    {
        if (isGenerated) return; 

        Vector3 floorCenter = Vector3.zero;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                GameObject randomPrefab = floors[Random.Range(5, 10)];
                
                Vector2 prefabSize = randomPrefab.GetComponent<Renderer>().bounds.size;
                Vector3 spawnPosition = new Vector3(x * prefabSize.x, y * prefabSize.y, 1);
                
                GameObject floor = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
                spawnedFloors[x, y] = floor;

                floorCenter += spawnPosition;
            }
        }

        floorCenter /= (width * height);

        if(playerPrefab != null)
        {
            //Debug.Log("posSpawn: " + playerPrefab.transform.position);
            Vector3 playerPosition = new Vector3(floorCenter.x, floorCenter.y, 0f);
            GameObject player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
            Debug.Log("Player position set to: " + player.transform.position);

            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.target = player.transform;
            }
        }

        isGenerated = true;
    }

    public void ChangeDayFloor()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int dayIndex = Random.Range(5, 10);
                Vector3 position = spawnedFloors[x, y].transform.position;

                Destroy(spawnedFloors[x, y]);
                GameObject dayFloor = Instantiate(floors[dayIndex], position, Quaternion.identity);
                spawnedFloors[x, y] = dayFloor;
            }
        }
    }

    public void ChangeNightFloor()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int nightIndex = Random.Range(0, 5);
                Vector3 position = spawnedFloors[x, y].transform.position;

                Destroy(spawnedFloors[x, y]);
                GameObject nightFloor = Instantiate(floors[nightIndex], position, Quaternion.identity);
                spawnedFloors[x, y] = nightFloor;
            }
        }
    }
}
