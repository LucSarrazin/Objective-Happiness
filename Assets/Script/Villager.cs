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
    public string[] nameList = new string[]
    {
        "Alexandre",
        "Eva",
        "Noah",
        "Sirius",
        "Ali",
        "Raphaël",
        "Solal",
        "Luc",
        "Zoé",
        "Nell",
        "Axel",
        "Alice",
        "Pauline",
        "Noémie",
        "Gaspard",
        "Samuel",
        "Hugo",
        "Ugo",
        "Alix",
        "Tom",
        "Elliot",
        "Lise",
        "Louison",
        "Cassandre",
        "Nathan",
        "Elouan",
        "Nelson",
        "Pablo",
        "Dorian",
        "Fabio",
        "Mylan",
        "Endy",
        "Mathéo",
        "Thomas",
        "Misha",
        "Jacques",
        "Moana",
        "Enzo",
        "Maël",
        "Ange",
        "Evann",
        "Guillaume",
        "Esteban",
        "Loutre",
        "Kerian"
    };

    [SerializeField] private AudioClip[] jobSounds;
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
    private bool onlyOneEat;
    private House houseIsSleeping;
    private bool isWorking;
    private bool isWalking;
    
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
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioSource walkingAudioSource;

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
        nameList = new string[]
        {
            "Alexandre",
            "Eva",
            "Noah",
            "Sirius",
            "Ali",
            "Raphaël",
            "Solal",
            "Luc",
            "Zoé",
            "Nell",
            "Axel",
            "Alice",
            "Pauline",
            "Noémie",
            "Gaspard",
            "Samuel",
            "Hugo",
            "Ugo",
            "Alix",
            "Tom",
            "Elliot",
            "Lise",
            "Louison",
            "Cassandre",
            "Nathan",
            "Elouan",
            "Nelson",
            "Pablo",
            "Dorian",
            "Fabio",
            "Mylan",
            "Endy",
            "Mathéo",
            "Thomas",
            "Misha",
            "Jacques",
            "Moana",
            "Enzo",
            "Maël",
            "Ange",
            "Evann",
            "Guillaume",
            "Esteban",
            "Loutre",
            "Kerian"
        };
    }

    private void OnEnable()
    {
        UpdateObjects();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        name = nameList[Random.Range(0, nameList.Length)];
        updateType();
        GameManager.Instance.Villagers.Add(this.gameObject);
        StartCoroutine(RandomWalk());
    }

    void updateType()
    {
        switch (type)
        {
            case types.vagrant:
                Debug.Log(name + " villager is vagrant");
                render.enabled = true;
                render.material = materialList[4];
                StopAllCoroutines();
                StartCoroutine("RandomWalk");
                break;
            case types.food_picker:
                Debug.unityLogger.Log(name + " villager is food picker");
                render.enabled = true;
                render.material = materialList[1];
                StopAllCoroutines();
                Debug.Log(name + " is now a food picker and going to take food");
                if (tired == false)
                {
                    agent.destination = foodList[Random.Range(0,foodList.Length)].transform.position;
                    agent.speed = 1f;
                    foodPlace = true;
                }
                else
                {
                    StartCoroutine("RandomWalk");
                }
                break;
            case types.lumberjack:
                Debug.Log(name + " villager is lumberjack");
                render.enabled = true;
                render.material = materialList[2];
                StopAllCoroutines();
                Debug.Log(name + " is now a lumberjack and going to take wood");
                if (tired == false)
                {
                    agent.destination = woodList[Random.Range(0,woodList.Length)].transform.position;
                    agent.speed = 1f;
                    woodPlace = true;
                }
                else
                {
                    StartCoroutine("RandomWalk");
                }
                break;
            case types.digger:
                Debug.Log(name + " villager is digger");
                render.enabled = true;
                render.material = materialList[0];
                StopAllCoroutines();
                Debug.Log(name + " is now a digger and going to mine rock");
                if (tired == false)
                {
                    agent.destination = rockList[Random.Range(0,rockList.Length)].transform.position;
                    agent.speed = 1f;
                    rockPlace = true;
                }
                else
                {
                    StartCoroutine("RandomWalk");
                }
                break;
            case types.mason:
                Debug.Log(name + " villager is mason");
                render.enabled = true;
                render.material = materialList[3];
                StopAllCoroutines();
                if (tired == false)
                {
                    StartCoroutine("needToBuild");
                }
                else
                {
                    StartCoroutine("RandomWalk");
                }
                break;
        }
    }

    public void UpdateObjects()
    {
        woodList = GameObject.FindGameObjectsWithTag("Wood");
        rockList = GameObject.FindGameObjectsWithTag("Rock");
        foodList = GameObject.FindGameObjectsWithTag("Food");
        houseList = GameObject.FindGameObjectsWithTag("House");
        schoolList = GameObject.FindGameObjectsWithTag("School");
    }
    
    private bool isChopping = false;
    IEnumerator cuttingTree()
    {
        while (woodPlace != true)
        {
            if (!isChopping && !agent.pathPending && agent.remainingDistance <= 0.2f)
            {
                isChopping = true;

                isWalking = false;
                isWorking = true;

                Debug.Log(name + " start chopping");

                audioSource.clip = jobSounds[1];
                audioSource.Play();

                yield return new WaitForSeconds(5f);

                audioSource.Stop();

                int wood = Random.Range(1, 4);
                GameManager.Instance.totalWood += wood;
                Debug.Log(name + " get " + wood);

                isWorking = false;
                isWalking = true;

                Transform nextWood = woodList[Random.Range(0, woodList.Length)].transform;
                agent.SetDestination(nextWood.position);

                yield return new WaitUntil(() => agent.remainingDistance > 1f);

                isChopping = false;
            }

            yield return null;
        }
    }
    
    private bool isMining = false;
    IEnumerator miningRock()
    {
        while (rockPlace != true)
        {
            if (!isMining && !agent.pathPending && agent.remainingDistance <= 0.2f)
            {
                isMining = true;
                isWalking = false;
                isWorking = true;
                Debug.Log(name + " start minning");
                audioSource.clip = jobSounds[3];
                audioSource.Play();
                yield return new WaitForSeconds(5f);
                audioSource.Stop();
                int rock = Random.Range(1, 4);
                GameManager.Instance.totalRock += rock;
                Debug.Log(name + " get " + rock);
                isWorking = false;
                isWalking = true;
                Transform nextRock = rockList[Random.Range(0,rockList.Length)].transform;
                agent.SetDestination(nextRock.position);
                yield return new WaitUntil(() => agent.remainingDistance > 1f);
                isMining = false;
            }
            yield return null;
        }
    }
    
    IEnumerator searchingFood()
    {
        while (foodPlace != true)
        {
            if (!isMining && !agent.pathPending && agent.remainingDistance <= 0.2f)
            {
                isMining = true;
                isWalking = false;
                isWorking = true;
                Debug.Log(name + " start searching food");
                audioSource.clip = jobSounds[0];
                audioSource.Play();
                yield return new WaitForSeconds(5f);
                audioSource.Stop();
                int rand = Random.Range(1, 4);
                float multiplier = Mathf.Pow(1.5f, GameManager.Instance.numberFarm);
                int food = Mathf.RoundToInt(rand * multiplier);
                GameManager.Instance.totalFood += food;
                Debug.Log(name + " get " + food);
                isWorking = false;
                isWalking = true;
                Transform nextFood = foodList[Random.Range(0, foodList.Length)].transform;
                agent.SetDestination(nextFood.position);
                yield return new WaitUntil(() => agent.remainingDistance > 1f);
                isMining = false;
            }
            yield return null;
        }
    }

    private bool oneTimePlay;
    // Update is called once per frame
    void Update()
    {
        if (age >= ageOfDeath)
        {
            tired = false;
            if (houseIsSleeping != null)
            {
                houseIsSleeping.sleeping = false;
            }
            Debug.Log(name + " is dead from age...");
            Destroy(this.gameObject);
        }

        if (needToEat == true)
        {
            tired = false;
            if (houseIsSleeping != null)
            {
                houseIsSleeping.sleeping = false;
            }
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
            audioSource.Stop();
            oneTime = false;
            tired = true;
            UpdateObjects();
            if (type != types.vagrant)
            {
                if (sleep == false && goToLearn == false)
                {
                    sleep = true;
                    goToLearn = false;
                    StopAllCoroutines();
                    masonUsed = false;
                    masonRunning = false;
                    Debug.Log("Début de la nuit");
                    woodPlace = false;
                    foodPlace = false;
                    rockPlace = false;
                    StartCoroutine("goBackHome");
                    age += 10;
                }
            }
            else if(type == types.vagrant)
            {
                StopAllCoroutines();
                StartCoroutine("RandomWalk");
                if (onlyOneEat == false)
                {
                    onlyOneEat = true;
                    age += 10;
                }
            }
        }
        else
        {
            if (oneTime == false)
            {
                oneTime = true;
                sleep = false;
                if (houseIsSleeping != null)
                {
                    houseIsSleeping.sleeping = false;
                    tired = false;
                }
                StopAllCoroutines();
                updateType();
            }
            
        }

        if (schoolList.Length == 0)
        {
           UIVillager.Instance.learnButtonUI.interactable = false; 
        }
        else if(schoolList.Length >= 1 && goToLearn == false)
        {
            UIVillager.Instance.learnButtonUI.interactable = true; 
        }
        
        
        int currentCount = GameManager.Instance.ListBuildingInConstruction.Count;

        if (currentCount > previousCount)
        {
            if (type == types.mason && masonUsed == false && tired == false)
            {
                masonUsed = true;
                StopAllCoroutines();
                StartCoroutine("needToBuild");
            }
        }

        previousCount = currentCount;

        // Handle animations

        if (isWorking)
        {
            oneTimePlay = false;
            animator.Play("house wiggle");
        }
        else if (isWalking)
        {
            animator.Play("walking chara");
            if (oneTimePlay == false)
            {
                oneTimePlay = true;
                audioSource.clip = jobSounds[4];
                audioSource.Play();
            }
        }
        else
        {
            animator.Play("idle");
            oneTimePlay = false;
            audioSource.Stop(); 
        }
    }


    IEnumerator RandomWalk()
    {
        isWalking = true;
        agent.speed = 1f;
        //Debug.Log("start Walking random");
        if (agent.hasPath == false)
        {
            agent.destination = new Vector3(Random.Range(-15,15),0,Random.Range(-15,15));
        }
        //Debug.Log("end Walking random");
        yield return new WaitForSeconds(1);
        isWalking = false;
        StartCoroutine(RandomWalk());
    }
    
    IEnumerator goBackHome()
    {
        if (type == types.mason)
        {
            StartCoroutine(goBackHomeMason());
            StopCoroutine(goBackHome());
        }

        yield return new WaitForSeconds(2f);
        Debug.Log("Go Back Home Villager");
        
        House house = null;
        GameObject home = null;
        bool foundHome = false;

        for (int i = 0; i < houseList.Length; i++)
        {
            GameObject homeTest = houseList[Random.Range(0, houseList.Length)];
            House houseTest = homeTest.GetComponent<House>();
            if (houseTest == null)
            {
                Debug.LogWarning("object in houselist without house script: " + homeTest.name);
                continue;
            }
            //Debug.Log("Test house: " + houseTest.name);

            bool isBuilding = false;
            if(homeTest.GetComponent<ConstructionSite>() != null)
            {
                isBuilding = homeTest.GetComponent<ConstructionSite>().enabled;
            }

            if (!houseTest.sleeping && !isBuilding)
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
            StopAllCoroutines();
            StartCoroutine("RandomWalk");
            yield break;
        }
        
        tired = false;
        house.sleeping = true;
        houseIsSleeping = house;
        agent.speed = 5f;
        agent.destination = home.transform.position;
        isWalking = true;
        if (agent != null)
        {
            
            while (agent.pathPending || agent.remainingDistance > 0.2f)
            {
                yield return null;
            }
            tired = false;
            isWalking = false;
            render.enabled = false;
            Debug.Log(name + " is sleeping");
        } 
    }
    
    IEnumerator goBackHomeMason()
    {
        StopCoroutine(goBackHome());
        Debug.Log("Go Back Home mason");
        
        House house = null;
        GameObject home = null;
        bool foundHome = false;

        for (int i = 0; i < houseList.Length; i++)
        {
            GameObject homeTest = houseList[Random.Range(0, houseList.Length)];
            House houseTest = homeTest.GetComponent<House>();
            if (houseTest == null)
            {
                Debug.LogWarning("object in houselist without house script: " + homeTest.name);
                continue;
            }
            //Debug.Log("Test house: " + houseTest.name);

            bool isBuilding = false;
            if(homeTest.GetComponent<ConstructionSite>() != null)
            {
                isBuilding = homeTest.GetComponent<ConstructionSite>().enabled;
            }

            if (!houseTest.sleeping && !isBuilding)
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
            StopAllCoroutines();
            StartCoroutine("RandomWalk");
            yield break;
        }
        
        tired = false;
        house.sleeping = true;
        houseIsSleeping = house;
        agent.speed = 5f;
        agent.destination = home.transform.position;
        isWalking = true;
        if (agent != null)
        {
            
            while (agent.pathPending || agent.remainingDistance > 0.2f)
            {
                yield return null;
            }
            tired = false;
            isWalking = false;
            render.enabled = false;
            Debug.Log(name + " is sleeping");
        } 
    }
    public void ShowInfo()
    {
        if (UIVillager.Instance == null) return;
        UIVillager.Instance.ShowInfo(this);
    }
    
    public void Touched()
    {
        if (UIVillager.Instance == null) return;

        UIVillager.Instance.ShowInfo(this);
        ShowInfo();
        Debug.Log(name + " has been touched");
    }
    IEnumerator startingSchool()
    {
        Debug.Log(name + " is starting school");
        int typeWanted = UIVillager.Instance.dropdownUI.value;
        School school = null;
        GameObject schoolPlace = null;
        bool foundSchool = false;

        for (int i = 0; i < schoolList.Length; i++)
        {
            GameObject schoolPlaceTest = schoolList[Random.Range(0, schoolList.Length)];
            School schoolTest = schoolPlaceTest.GetComponent<School>();
            
            bool isBuilding = false;
            if(schoolTest.GetComponent<ConstructionSite>() != null)
            {
               isBuilding = schoolTest.GetComponent<ConstructionSite>().enabled;
            }
            if (!schoolTest.maxStudent && !isBuilding)
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
            updateType();
            yield break;
        }
        
        Debug.Log(name + " find school");
        if (type == types.mason)
        {
            GameManager.Instance.numberMason--;
        }
        switch (typeWanted)
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
                GameManager.Instance.numberMason++;
                type = types.mason;
                break;
        }
        
        school.maxStudent = true;
        agent.speed = 3f;
        agent.destination = schoolPlace.transform.position;
        isWalking = true;

        while (agent.pathPending || agent.remainingDistance > 0.2f)
        {
            yield return null;
        }
        isWalking = false;
        Debug.Log(name + " is studying");
        agent.speed = 0f;
        render.enabled = false;
        yield return new WaitForSeconds(numberOfTimeToLearn);
        Debug.Log(name + " stop studying");
        render.enabled = true;
        school.maxStudent = false;
        updateType();
        UIVillager.Instance.learnButtonUI.interactable = true;
        goToLearn = false;
        agent.speed = 1f;
        Debug.Log(name + " finished studying");
    }
        
    #region MasonBuildRegion
    private bool masonRunning = false;

    IEnumerator needToBuild()
    {
        if (GameManager.Instance.night == false)
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
                isWalking = true;

                while (agent.pathPending || agent.remainingDistance > 0.2f)
                    yield return null;

                isWorking = true;
                audioSource.clip = jobSounds[2];
                audioSource.Play();
                isWalking = false;
                while (constructionSite != null && 
                       GameManager.Instance.ListBuildingInConstruction.Contains(masonPlace))
                {
                    yield return null;
                }
            }

            audioSource.Stop();
            isWorking = false;
            masonUsed = false;
            StartCoroutine("RandomWalk");
            masonRunning = false;
        }
    }
    #endregion

        
}
