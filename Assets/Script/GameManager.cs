using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static int days = 1;
    static float minutes;
    static int totalWood = 5;
    static int totalRock = 5;
    static int totalFood = 5;
    static int totalProgress = 0;
    static int numberMason = 1;
    static int totalPopulation = 5;
    
    public float elapsedTime = 0f;
    
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
    }

    void StartDay()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Timer days of 5 minutes
        
        minutes -= Time.deltaTime;
        elapsedTime = Mathf.Floor(minutes / 60);
        if (minutes <= 0)
        {
            Debug.Log("Nul!");
        }
        Debug.Log(elapsedTime);
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
