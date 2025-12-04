using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int days;
    public float minutes;
    public int totalWood;
    public int totalRock;
    public int totalFood;
    public int totalProgress;
    public int numberMason;
    public int totalPopulation;
    
    // Start is called before the first frame update
    void Start()
    {
        days = 1;
        totalFood = 5;
        totalRock = 5;
        totalWood = 5;
        totalProgress = 0;
        numberMason = 1;
        totalPopulation = 5;
    }

    void StartDay()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
