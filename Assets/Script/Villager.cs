using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
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
    public float tiredness = 100;
    public float hunger = 100;
    public int age = 10;
    public int ageOfDeath;
    
    [Space]
    
    [SerializeField] private bool woodPlace;
    [SerializeField] private bool rockPlace;
    [SerializeField] private bool foodPlace;
    [SerializeField] private GameObject[] woodList;
    [SerializeField] private GameObject[] rockList;
    [SerializeField] private GameObject[] foodList;
    
    [Space]
    
    [Header("Villager Unity Parameters")]
    private NavMeshAgent agent;
    [SerializeField] private MeshRenderer render;
    [SerializeField] private SpriteRenderer render1;
    
    [Space]
    
    [Header("Villager UI Parameters")]
    [SerializeField] private TextMeshProUGUI nameUI;
    [SerializeField] private TextMeshProUGUI typeUI;
    [SerializeField] private Slider hungrySlider;
    [SerializeField] private TextMeshProUGUI ageUI;
    [SerializeField] private GameObject UI;
    
    private void OnValidate()
    {
        if (gameObject.scene.rootCount == 0)
            return;
        updateType();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        woodList = GameObject.FindGameObjectsWithTag("Wood");
        rockList = GameObject.FindGameObjectsWithTag("Rock");
        foodList = GameObject.FindGameObjectsWithTag("Food");
        updateType();
        StartCoroutine(RandomWalk());
    }

    void updateType()
    {
        switch (type)
        {
            case types.vagrant:
                Debug.Log(name + " villager is vagrant");
                render1.color = Color.white;
                render.material.color = Color.white;
                StopAllCoroutines();
                break;
            case types.food_picker:
                Debug.unityLogger.Log(name + " villager is food picker");
                render1.color = Color.blue;
                render.material.color = Color.blue;
                StopAllCoroutines();
                Debug.Log(name + " is now a food picker and going to take food");
                agent.destination = foodList[Random.Range(0,foodList.Length)].transform.position;
                foodPlace = true;
                break;
            case types.lumberjack:
                Debug.Log(name + " villager is lumberjack");
                render1.color = Color.green;
                render.material.color = Color.green;
                StopAllCoroutines();
                Debug.Log(name + " is now a lumberjack and going to take wood");
                agent.destination = woodList[Random.Range(0,woodList.Length)].transform.position;
                woodPlace = true;
                break;
            case types.digger:
                Debug.Log(name + " villager is digger");
                render1.color = Color.yellow;
                render.material.color = Color.yellow;
                StopAllCoroutines();
                Debug.Log(name + " is now a digger and going to mine rock");
                agent.destination = rockList[Random.Range(0,rockList.Length)].transform.position;
                rockPlace = true;
                break;
            case types.mason:
                Debug.Log(name + " villager is mason");
                render1.color = Color.black;
                render.material.color = Color.black;
                StopAllCoroutines();
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
                Debug.Log(name + " get " + Random.Range(1,6));
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
                Debug.Log(name + " get " + Random.Range(1,6));
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
                Debug.Log(name + " get " + Random.Range(1,6));
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
        }

        if (hunger <= 0)
        {
            Debug.Log(name + " is dead from hunger...");
        }
        UpdateInfo();

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
    }

    IEnumerator RandomWalk()
    {
        //Debug.Log("start Walking random");
        if (agent.hasPath == false)
        {
            agent.destination = new Vector3(Random.Range(-15,15),0,Random.Range(-15,15));
        }
        //Debug.Log("end Walking random");
        yield return new WaitForSeconds(1);
        StartCoroutine(RandomWalk());
    }

    private void UpdateInfo()
    {
         nameUI.text = name;
         typeUI.text = type.ToString();
         hungrySlider.value = hunger;
         ageUI.text = age.ToString();
    }

    private void ShowInfo()
    {
        UI.SetActive(true);
    }

    public void HideInfo()
    {
        UI.SetActive(false);
    }
    
    public void Touched()
    {
        UpdateInfo();
        ShowInfo();
        Debug.Log(name + " has been touched");
        
    }

    public void LearnButton()
    {
        
    }
    
    
    
}
