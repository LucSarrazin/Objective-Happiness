using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class House : MonoBehaviour
{
    [Header("House Parameters")]
    public string name;
    public string type = "House";
    public bool sleeping;
    // Start is called before the first frame update
    void Start()
    {
    }
    
    public void Touched()
    {
        Debug.Log(name + " has been touched");
        if (sleeping == true)
        {
            Debug.Log("Someone is sleeping in " + name);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
