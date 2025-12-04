using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int days = 1;
    public float minutes;
    public int totalWood = 5;
    public int totalRock = 5;
    public int totalFood = 5;
    public int totalProgress = 0;
    public int numberMason = 1;
    public int totalPopulation = 5;
    
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            Time.timeScale = 0;
        }
    }
}
