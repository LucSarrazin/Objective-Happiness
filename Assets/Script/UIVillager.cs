using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVillager : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] public Villager Villager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void LearnButton()
    {
        Villager.goToLearn = true;
        HideInfo();
        Villager.learnButtonUI.interactable = false;
        StopAllCoroutines();
        StartCoroutine("startingSchool");
    }

    public void ShowInfo()
    {
        UI.SetActive(true);
    }

    public void HideInfo()
    {
        UI.SetActive(false);
    }
}
