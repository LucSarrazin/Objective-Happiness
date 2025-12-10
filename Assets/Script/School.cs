using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class School : MonoBehaviour
{
    [Header("School Parameters")]
    public string name;
    public string type = "School";
    public bool maxStudent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void Touched()
    {
        Debug.Log(name + " has been touched");
        if (maxStudent == true)
        {
            Debug.Log("Someone is studing in " + name);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
