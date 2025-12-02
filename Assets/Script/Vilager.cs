using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vilager : MonoBehaviour
{
    [Header("Vilager Parameters")]
    public string name;

    public enum types
    {
        village,
        food_picker,
        lumberjack,
        digger,
        mason,
        vagrant
    };
    public types type;
    public float tiredness;
    public float food;
    public int age;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
