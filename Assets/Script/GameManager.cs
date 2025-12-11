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

    public readonly float requieredProgress = 100f;


    public float totalProgress = 0f;
    public bool night = false;
    public List<GameObject> ListBuildingInConstruction = new List<GameObject>();
    public GameObject[] ListFarm;
    private int previousCount = 0;
    public int numberFarm = 0;
    public GameObject directionLight;
    public GameObject plane;
    
    public float elapsedTime = 0f;

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
    
    void StartDay()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update Progress

        if (totalProgress < requieredProgress)
            totalProgress += Time.deltaTime * 5; // THIS IS JUST FOR TESTING PURPOSES, DO NOT SHIP, REMOVE LATER

        // Check victory

        // Update Time

        elapsedTime += Time.deltaTime;

        night = elapsedTime >= dayDuration;
        if (night)
            directionLight.transform.rotation = Quaternion.Euler(sunRotation.x + (elapsedTime - dayDuration) / nightDuration * 360f, sunRotation.y, sunRotation.z);
        
        if (elapsedTime >= dayDuration + nightDuration) // New Day
        {
            // Consume food and kill surplus

            // Check for defeat

            // Letter here
            // Pause time to show letter

            // Reset time
            days++;
            elapsedTime = 0f;
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
        Time.timeScale = currentTimeScale;
    }
}
