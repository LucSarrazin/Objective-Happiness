using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int days = 1;
    public float dayDuration = 300;
    private float sunRotationSpeed;
    public int totalWood = 5;
    public int totalRock = 5;
    public int totalFood = 5;
    public int numberMason = 1;
    public int totalPopulation = 5;

    public readonly float requieredProgress = 100f;


    public float totalProgress = 0f;
    [SerializeField] private bool dayStart = false;
    public bool night = false;
    public List<GameObject> ListBuildingInConstruction = new List<GameObject>();
    public GameObject[] ListFarm;
    private int previousCount = 0;
    public int numberFarm = 0;
    public GameObject directionLight;
    public GameObject plane;
    
    public float elapsedTime = 0f;
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
        sunRotationSpeed = 360f / dayDuration;
        days = 1;
        dayStart = true;
        totalFood = 5;
        totalRock= 5;
        totalWood = 5;
        totalProgress = 0;
        numberMason = 1;
        totalPopulation = 5;
        numberFarm = 0;
    }
    
    void StartDay()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (totalProgress < requieredProgress)
            totalProgress += Time.deltaTime * 5; // THIS IS JUST FOR TESTING PURPOSES, DO NOT SHIP, REMOVE LATER

        // Timer days of 5 minutes
        if (dayStart == true)
        {
            dayDuration -= Time.deltaTime;
            elapsedTime = Mathf.Floor(dayDuration / 60);
            if (dayDuration <= 0)
            {
                Debug.Log("Nul!");
                dayStart = false;
                night = true;
                if (totalPopulation > 0)
                {
                    // Faire POP le journal ici
                    days += 1;  
                }
                else
                {
                    
                }
                
            }
            Debug.Log(elapsedTime);
            directionLight.transform.RotateAround(plane.transform.position, Vector3.right, Time.deltaTime * sunRotationSpeed);
            
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
        Time.timeScale = 1;
    }

    public void DoubleTime()
    {
        Time.timeScale = 2;
    }

    public void TripleTime()
    {
        Time.timeScale = 3;
    }
 
    
}
