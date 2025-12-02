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
    [Space]
    [Header("House UI Parameters")]
    [SerializeField] private TextMeshProUGUI nameUI;
    [SerializeField] private TextMeshProUGUI typeUI;
    [SerializeField] private Toggle sleepingUI;
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
        sleepingUI.isOn = sleeping;
    }

    private void ShowInfo()
    {
        UI.SetActive(true);
    }

    public void HideInfo()
    {
        UI.SetActive(false);
    }
    
    public void touched()
    {
        UpdateInfo();
        ShowInfo();
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
