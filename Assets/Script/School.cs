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
    [Space]
    [Header("School UI Parameters")]
    [SerializeField] private TextMeshProUGUI nameUI;
    [SerializeField] private TextMeshProUGUI typeUI;
    [SerializeField] private Toggle studingUI;
    [SerializeField] private GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        nameUI.text = name;
        typeUI.text = type;
        studingUI.isOn = maxStudent;
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
