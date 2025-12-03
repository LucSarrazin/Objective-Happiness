using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vilager : MonoBehaviour
{
    [Header("Vilager Parameters")]
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
    public float food = 100;
    public int age = 50;
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(RandomWalk());
    }

    // Update is called once per frame
    void Update()
    {
        
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
