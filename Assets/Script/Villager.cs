using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    
    
    [SerializeField] private bool woodPlace;
    [SerializeField] private bool rockPlace;
    [SerializeField] private GameObject wood;
    [SerializeField] private GameObject rock;
    
    [Header("Villager Objects")]
    private NavMeshAgent agent;
    [SerializeField] private MeshRenderer render;
    [SerializeField] private SpriteRenderer render1;
    
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
        wood = GameObject.FindGameObjectWithTag("Wood");
        updateType();
        StartCoroutine(RandomWalk());
    }

    void updateType()
    {
        switch (type)
        {
            case types.vagrant:
                Debug.Log(name + "villager is vagrant");
                render1.color = Color.white;
                render.material.color = Color.white;
                StopAllCoroutines();
                break;
            case types.food_picker:
                Debug.unityLogger.Log(name + "villager is food picker");
                render1.color = Color.blue;
                render.material.color = Color.blue;
                StopAllCoroutines();
                break;
            case types.lumberjack:
                Debug.Log(name + "villager is lumberjack");
                render1.color = Color.green;
                render.material.color = Color.green;
                StopAllCoroutines();
                Debug.Log(name + " is now a lumberjack and going to take wood");
                agent.destination = wood.transform.position;
                woodPlace = true;
                break;
            case types.digger:
                Debug.Log(name + "villager is digger");
                render1.color = Color.yellow;
                render.material.color = Color.yellow;
                StopAllCoroutines();
                Debug.Log(name + " is now a digger and going to mine rock");
                agent.destination = rock.transform.position;
                rockPlace = true;
                break;
            case types.mason:
                Debug.Log(name + "villager is mason");
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
            Debug.Log(name + " start chooping");
            yield return new WaitForSeconds(5f);
            Debug.Log(name + " a recupéré " + Random.Range(1,11));
        }
    }
    
    IEnumerator miningRock()
    {
        while (woodPlace != true)
        {
            Debug.Log(name + " start minning");
            yield return new WaitForSeconds(5f);
            Debug.Log(name + " a recupéré " + Random.Range(1,11));
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
    }

    IEnumerator RandomWalk()
    {
        Debug.Log("start Walking random");
        if (agent.hasPath == false)
        {
            agent.destination = new Vector3(Random.Range(-15,15),0,Random.Range(-15,15));
        }
        Debug.Log("end Walking random");
        yield return new WaitForSeconds(1);
        StartCoroutine(RandomWalk());
    }
    
    
    
}
