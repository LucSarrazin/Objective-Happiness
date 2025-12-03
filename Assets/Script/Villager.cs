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

    public enum types
    {
        villager,
        food_picker,
        lumberjack,
        digger,
        mason,
        vagrant
    };
    public types type;
    public float tiredness = 100;
    public float hunger = 100;
    public int age = 10;
    public int ageOfDeath;
    
    [Header("Villager Objects")]
    private NavMeshAgent agent;
    [SerializeField] private MeshRenderer render;
    
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
        updateType();
        StartCoroutine(RandomWalk());
    }

    void updateType()
    {
        switch (type)
        {
            case types.villager:
                Debug.Log(name + " villager is villager");
                render.material.color = Color.white;
                break;
            case types.food_picker:
                Debug.unityLogger.Log(name + "villager is food picker");
                render.material.color = Color.blue;
                break;
            case types.lumberjack:
                Debug.Log(name + "villager is lumberjack");
                render.material.color = Color.green;
                break;
            case types.digger:
                Debug.Log(name + "villager is digger");
                render.material.color = Color.yellow;
                break;
            case types.mason:
                Debug.Log(name + "villager is mason");
                render.material.color = Color.black;
                break;
            case types.vagrant:
                Debug.Log(name + "villager is vagrant");
                render.material.color = Color.red;
                break;
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
