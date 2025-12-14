using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIVillager : MonoBehaviour
{
    public static UIVillager Instance;
    [SerializeField] private GameObject UI;
    [SerializeField] public Villager Villager;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI nameUI;
    [SerializeField] private TextMeshProUGUI typeUI;
    [SerializeField] private TextMeshProUGUI ageUI;
    [SerializeField] public TMP_Dropdown dropdownUI;
    [SerializeField] public Button learnButtonUI;

    void Awake()
    {
        Instance = this;
        UI.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInfo(Villager villager)
    {
        Villager = villager;
        UpdateInfo();
        UI.SetActive(true);
    }

    public void HideInfo()
    {
        UI.SetActive(false);
        Villager = null;
    }

    void UpdateInfo()
    {
        nameUI.text = Villager.name;
        typeUI.text = Villager.type.ToString();
        ageUI.text = Villager.age.ToString();
    }

    public void LearnButton()
    {
        if (Villager == null) return;
        Villager.goToLearn = true;
        HideInfo();
    }
}
