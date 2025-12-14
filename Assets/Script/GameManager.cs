using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int days = 1;
    public float dayDuration = 270;
    public float nightDuration = 30;
    public int totalWood = 5;
    public int totalRock = 5;
    public int totalFood = 5;
    public int numberMason = 1;
    public int totalPopulation = 5;
    [SerializeField] private GameObject villagerPrefab;

    public readonly float requieredProgress = 100f;


    public float totalProgress = 0f;
    public bool night = false;
    public List<GameObject> ListBuildingInConstruction = new List<GameObject>();
    public List<GameObject> Villagers = new List<GameObject>();
    public GameObject[] ListFarm;
    private int previousCount = 0;
    public int numberFarm = 0;
    public GameObject directionLight;
    public GameObject plane;
    
    public float elapsedTime = 0f;

    [SerializeField] private LevelLoader levelLoader;

    private float currentTimeScale = 1;
    private Vector3 sunRotation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        days = 1;
        totalFood = 5;
        totalRock= 5;
        totalWood = 5;
        totalProgress = 0;
        numberMason = 1;
        totalPopulation = 5;
        numberFarm = 0;

        sunRotation = directionLight.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Check victory
        if (totalProgress >= requieredProgress)
        {
            levelLoader.LoadLevelByName("GoodEnding");
            return;
        }
        //else
        //    totalProgress += Time.deltaTime * 5; // THIS IS JUST FOR TESTING PURPOSES, DO NOT SHIP, REMOVE LATER

        // Update Time

        elapsedTime += Time.deltaTime;

        night = elapsedTime >= dayDuration;
        if (night)
            directionLight.transform.rotation = Quaternion.Euler(sunRotation.x + (elapsedTime - dayDuration) / nightDuration * 360f, sunRotation.y, sunRotation.z);
        
        if (elapsedTime >= dayDuration + nightDuration) // New Day
        {
            // Consume food and kill surplus
            totalFood -= Villagers.Count;

            if (totalFood < 0)
            {
                int deaths = -totalFood;
                totalFood = 0;

                for (int i = 0; i < deaths && Villagers.Count > 0; i++)
                {
                    GameObject villager = Villagers[Random.Range(0, Villagers.Count)];
                    Villagers.Remove(villager);
                    Destroy(villager);
                    totalPopulation--;
                }
            }

            // Update Progress
            for (int i = 0; i < Villagers.Count; i++)
            {
                if (Villagers[i].GetComponent<Villager>().tired)
                    totalProgress -= 1f;
                else
                    totalProgress += 2f; // game is too hard :(
            }

            totalProgress = Mathf.Max(0f, totalProgress);
            
            // Check for defeat
            if (totalPopulation <= 0)
                levelLoader.LoadLevelByName("BadEnding");

            // Letter here
            // Pause time to show letter
            
            //Spawn villagers
            VillagerSpawn();

            // Reset time
            elapsedTime = 0f;

            days++;
        }

        if (ListFarm.Length > previousCount)
        {
            ListFarm = GameObject.FindGameObjectsWithTag("Farm");
            for (int i = 0; i < ListFarm.Length; i++)
            {
                if (ListFarm[i].GetComponent<ConstructionSite>().isActiveAndEnabled == false)
                {
                    numberFarm++;
                }
            }
        }
        previousCount = ListFarm.Length;
    }


    void VillagerSpawn()
    {
        int spawnCount = Random.Range(1, 4);

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject villager = Instantiate(villagerPrefab);
            villager.transform.position = new Vector3(
                Random.Range(-5, 5),
                0,
                Random.Range(-5, 5)
            );

            Villagers.Add(villager);
            totalPopulation++;
        }
    }

    
    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void StartTime()
    {
        Time.timeScale = currentTimeScale;
    }

    public void SetTimeSpeed(float speed)
    {
        currentTimeScale = speed;
        if(Time.timeScale != 0)
            Time.timeScale = currentTimeScale;
    }
}
