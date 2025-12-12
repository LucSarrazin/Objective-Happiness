using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Villager : MonoBehaviour
{
    [Header("Villager Parameters")]
    public string name;
    //private GameManager gameManager;

    public enum types
    {
        vagrant,
        food_picker,
        lumberjack,
        digger,
        mason
    };
    public types type;
    public Sprite[] spriteList;
    public Material[] materialList;
    public bool tired;
    public bool hungry;
    public bool needToEat;
    public int age = 10;
    public int ageOfDeath;
    public bool sleep = false;
    public float numberOfTimeToLearn;
    public bool goToLearn;
    private int previousCount = 0;
    private bool masonUsed;
    private bool oneTime;
    
    [Space]
    
    [SerializeField] private bool woodPlace;
    [SerializeField] private bool rockPlace;
    [SerializeField] private bool foodPlace;
    [SerializeField] private GameObject[] woodList;
    [SerializeField] private GameObject[] rockList;
    [SerializeField] private GameObject[] foodList;
    [SerializeField] private GameObject[] houseList;
    [SerializeField] private GameObject[] schoolList;
    [SerializeField] private GameObject[] buildingInConstruction;
    
    [Space]
    
    [Header("Villager Unity Parameters")]
    private NavMeshAgent agent;
    [SerializeField] private MeshRenderer render;
    [SerializeField] private SpriteRenderer render1;
    
    [Space]
    
    [Header("Villager UI Parameters")]
    [SerializeField] private TextMeshProUGUI nameUI;
    [SerializeField] private TextMeshProUGUI typeUI;
    [SerializeField] private TextMeshProUGUI ageUI;
    [SerializeField] private GameObject UI;
    [SerializeField] private UIVillager villagerUI;
    [SerializeField] private TMP_Dropdown dropdownUI;
    public Button learnButtonUI;
    
    // private void OnValidate()
    // {
    //     if (gameObject.scene.rootCount == 0)
    //         return;
    //     updateType();
    // }

    private void Awake()
    {
        woodList = GameObject.FindGameObjectsWithTag("Wood");
        rockList = GameObject.FindGameObjectsWithTag("Rock");
        foodList = GameObject.FindGameObjectsWithTag("Food");
        houseList = GameObject.FindGameObjectsWithTag("House");
        schoolList = GameObject.FindGameObjectsWithTag("School");


        nameUI = GameObject.FindGameObjectWithTag("NameUIVillager").GetComponent<TextMeshProUGUI>();
        typeUI = GameObject.FindGameObjectWithTag("TypeUIVillager").GetComponent<TextMeshProUGUI>();
        ageUI = GameObject.FindGameObjectWithTag("AgeUIVillager").GetComponent<TextMeshProUGUI>();
        UI = GameObject.FindGameObjectWithTag("UIVillager");
        villagerUI = GameObject.FindGameObjectWithTag("UI").GetComponent<UIVillager>();
        dropdownUI = GameObject.FindGameObjectWithTag("DropdownUIVillager").GetComponent<TMP_Dropdown>();
        learnButtonUI = GameObject.FindGameObjectWithTag("LearnButtonUIVillager").GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        updateType();
        GameManager.Instance.Villagers.Add(this.gameObject);
        StartCoroutine(RandomWalk());
        UI.SetActive(false);
    }

    void updateType()
    {
        switch (type)
        {
            case types.vagrant:
                Debug.Log(name + " villager is vagrant");
                render.material = materialList[4];
                StopAllCoroutines();
                StartCoroutine("RandomWalk");
                break;
            case types.food_picker:
                Debug.unityLogger.Log(name + " villager is food picker");
                render.material = materialList[1];
                StopAllCoroutines();
                Debug.Log(name + " is now a food picker and going to take food");
                agent.destination = foodList[Random.Range(0,foodList.Length)].transform.position;
                foodPlace = true;
                break;
            case types.lumberjack:
                Debug.Log(name + " villager is lumberjack");
                render.material = materialList[2];
                StopAllCoroutines();
                Debug.Log(name + " is now a lumberjack and going to take wood");
                agent.destination = woodList[Random.Range(0,woodList.Length)].transform.position;
                woodPlace = true;
                break;
            case types.digger:
                Debug.Log(name + " villager is digger");
                render.material = materialList[0];
                StopAllCoroutines();
                Debug.Log(name + " is now a digger and going to mine rock");
                agent.destination = rockList[Random.Range(0,rockList.Length)].transform.position;
                rockPlace = true;
                break;
            case types.mason:
                Debug.Log(name + " villager is mason");
                render.material = materialList[3];
                GameManager.Instance.numberMason++;
                StopAllCoroutines();
                StartCoroutine("RandomWalk");
                break;
        }
    }
    IEnumerator cuttingTree()
    {
        while (woodPlace != true)
        {
            if (agent.hasPath == false)
            {
                Debug.Log(name + " start chooping");
                yield return new WaitForSeconds(5f);
                int wood = Random.Range(1, 4);
                GameManager.Instance.totalWood += wood;
                Debug.Log(name + " get " + wood);
                agent.destination = woodList[Random.Range(0,woodList.Length)].transform.position;
            }
            yield return null;
        }
    }
    
    IEnumerator miningRock()
    {
        while (rockPlace != true)
        {
            if (agent.hasPath == false)
            {
                Debug.Log(name + " start minning");
                yield return new WaitForSeconds(5f);
                int rock = Random.Range(1, 4);
                GameManager.Instance.totalRock += rock;
                Debug.Log(name + " get " + rock);
                agent.destination = rockList[Random.Range(0,rockList.Length)].transform.position;
            }
            yield return null;
        }
    }
    
    IEnumerator searchingFood()
    {
        while (foodPlace != true)
        {
            if (agent.hasPath == false)
            {
                Debug.Log(name + " start searching food");
                yield return new WaitForSeconds(5f);
                int rand = Random.Range(1, 4);
                float multiplier = Mathf.Pow(1.5f, GameManager.Instance.numberFarm);
                int food = Mathf.RoundToInt(rand * multiplier);
                GameManager.Instance.totalFood += food;
                Debug.Log(name + " get " + food);
                agent.destination = foodList[Random.Range(0,foodList.Length)].transform.position;
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (age >= ageOfDeath)
        {
            Debug.Log(name + " is dead from age...");
            Destroy(this.gameObject);
        }

        if (needToEat == true)
        {
            Debug.Log(name + " is dead from eat...");
            Destroy(this.gameObject);
        }

        // Three Fonction to verify that the villager is where there is food/tree/rock and stop there
        if (woodPlace == true && agent.hasPath == false)
        {
            woodPlace = false;
            StopAllCoroutines();
            StartCoroutine("cuttingTree");
        }
        if (rockPlace == true && agent.hasPath == false)
        {
            rockPlace = false;
            StopAllCoroutines();
            StartCoroutine("miningRock");
        }
        if (foodPlace == true && agent.hasPath == false)
        {
            foodPlace = false;
            StopAllCoroutines();
            StartCoroutine("searchingFood");
        }

        if (GameManager.Instance.night == true)
        {
            oneTime = false;
            tired = true;
            if (type != types.vagrant)
            {
                if (sleep == false)
                {
                    sleep = true;
                    StopAllCoroutines();
                    Debug.Log("Début de la nuit");
                    woodPlace = false;
                    foodPlace = false;
                    rockPlace = false;
                    StartCoroutine("goBackHome");
                }
            }
            else
            {
                if (GameManager.Instance.totalFood <= 0)
                {
                    Debug.Log(name + " is dead from hunger...");
                    Destroy(this.gameObject);
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine("RandomWalk");
                    GameManager.Instance.totalFood--;
                    GameManager.Instance.Villagers.Remove(this.gameObject);
                }
            }
        }
        else
        {
            if (oneTime == false)
            {
                oneTime = true;
                updateType();
            }
            
        }

        if (schoolList.Length == 0)
        {
           learnButtonUI.interactable = false; 
        }
        else if(schoolList.Length >= 1 && goToLearn == false)
        {
            learnButtonUI.interactable = true; 
        }
        
        
        int currentCount = GameManager.Instance.ListBuildingInConstruction.Count;

        if (currentCount > previousCount)
        {
            if (type == types.mason && masonUsed == false)
            {
                masonUsed = true;
                StopAllCoroutines();
                StartCoroutine("needToBuild");
            }
        }

        previousCount = currentCount;
        
    }
    

    IEnumerator RandomWalk()
    {
        agent.speed = 1f;
        //Debug.Log("start Walking random");
        if (agent.hasPath == false)
        {
            agent.destination = new Vector3(Random.Range(-15,15),0,Random.Range(-15,15));
        }
        //Debug.Log("end Walking random");
        yield return new WaitForSeconds(1);
        StartCoroutine(RandomWalk());
    }
    
    IEnumerator goBackHome()
    {
        Debug.Log("Go Back Home Villager");
        
        House house = null;
        GameObject home = null;
        bool foundHome = false;

        for (int i = 0; i < houseList.Length; i++)
        {
            GameObject homeTest = houseList[Random.Range(0, houseList.Length)];
            House houseTest = homeTest.GetComponent<House>();
            Debug.Log("Test house: " + houseTest.name);

            if (!houseTest.sleeping)
            {
                home = homeTest;
                house = houseTest;
                foundHome = true;
                break;
            }
        }
        
        if (!foundHome)
        {
            Debug.LogWarning(name + " has no house available !");
            tired = true;
            
            if (GameManager.Instance.totalFood <= 0)
            {
                Debug.Log(name + " is dead from hunger...");
                Destroy(this.gameObject);
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine("RandomWalk");
                GameManager.Instance.totalFood--;
                GameManager.Instance.Villagers.Remove(this.gameObject);
            }
            StartCoroutine("RandomWalk");
            yield break;
        }
        
        house.sleeping = true;
        agent.speed = 5f;
        agent.destination = home.transform.position;
        if (agent.isStopped == true)
        {
            yield return null;
            tired = false;
            Debug.Log(name + " is sleeping");
        }
        yield return null;
    }

    private void UpdateInfo()
    {
         nameUI.text = name;
         typeUI.text = type.ToString();
         ageUI.text = age.ToString();
    }

    public void HideInfo()
    {
        villagerUI.HideInfo();
    }

    public void ShowInfo()
    {
        UpdateInfo();
        villagerUI.ShowInfo();
    }
    
    public void Touched()
    {
        villagerUI.Villager = this;
        UpdateInfo();
        ShowInfo();
        Debug.Log(name + " has been touched");
    }

    IEnumerator startingSchool()
    {
        Debug.Log(name + " is starting school");
        School school = null;
        GameObject schoolPlace = null;
        bool foundSchool = false;

        for (int i = 0; i < schoolList.Length; i++)
        {
            GameObject schoolPlaceTest = schoolList[Random.Range(0, schoolList.Length)];
            School schoolTest = schoolPlaceTest.GetComponent<School>();

            if (!schoolTest.maxStudent)
            {
                schoolPlace = schoolPlaceTest;
                school = schoolTest;
                foundSchool = true;
                break;
            }
        }
        
        if (!foundSchool)
        {
            Debug.LogWarning(name + " has no school available !");
            StartCoroutine("RandomWalk");
            yield break;
        }
        
        Debug.Log(name + " find school");
        if (type == types.mason)
        {
            GameManager.Instance.numberMason--;
        }
        switch (dropdownUI.value)
        {
            case 0:
                type = types.food_picker;
                break;
            case 1:
                type = types.lumberjack;
                break;
            case 2:
                type = types.digger;
                break;
            case 3:
                type = types.mason;
                break;
        }
        
        school.maxStudent = true;
        agent.speed = 3f;
        agent.destination = schoolPlace.transform.position;

        while (agent.pathPending || agent.remainingDistance > 0.2f)
        {
            yield return null;
        }
        Debug.Log(name + " is studying");
        render.enabled = false;
        yield return new WaitForSeconds(numberOfTimeToLearn);
        Debug.Log(name + " stop studying");
        render.enabled = true;
        school.maxStudent = false;
        updateType();
        learnButtonUI.interactable = true;
        goToLearn = false;
        agent.speed = 1f;
        Debug.Log(name + " finished studying");
    }
    
#region MasonBuildRegion
private bool masonRunning = false;

IEnumerator needToBuild()
{
    if (masonRunning) yield break;
    masonRunning = true;
    
    while (GameManager.Instance.ListBuildingInConstruction.Count > 0)
    {
        GameObject masonPlace = null;
        ConstructionSite constructionSite = null;
        int highestNeedForMasons = 0;
        float bestDistance = Mathf.Infinity;
        
        for (int i = 0; i < GameManager.Instance.ListBuildingInConstruction.Count; i++)
        {
            GameObject building = GameManager.Instance.ListBuildingInConstruction[i];
            ConstructionSite site = building.GetComponent<ConstructionSite>();
            
            if (site != null && site.masonCount < site.buildingCosts.requiredMason)
            {
                int masonNeeded = site.buildingCosts.requiredMason - site.masonCount;
                float dist = Vector3.Distance(transform.position, building.transform.position);
                
                if (masonNeeded > highestNeedForMasons || 
                    (masonNeeded == highestNeedForMasons && dist < bestDistance))
                {
                    highestNeedForMasons = masonNeeded;
                    bestDistance = dist;
                    constructionSite = site;
                    masonPlace = building;
                }
            }
        }
        
        if (masonPlace == null || highestNeedForMasons == 0)
            break;
        
        agent.speed = 5f;
        agent.SetDestination(masonPlace.transform.position);
        
        while (agent.pathPending || agent.remainingDistance > 0.2f)
            yield return null;
        
        while (constructionSite != null && 
               constructionSite.assignedMasons.Contains(this) && 
               GameManager.Instance.ListBuildingInConstruction.Contains(masonPlace))
        {
            yield return null;
        }
        
        yield return new WaitForSeconds(0.5f);
    }
    
    masonUsed = false;
    StartCoroutine("RandomWalk");
    masonRunning = false;
}
#endregion
        
}
