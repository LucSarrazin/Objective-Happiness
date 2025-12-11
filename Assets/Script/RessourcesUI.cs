using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RessourcesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rockCounter;
    [SerializeField] private TextMeshProUGUI woodCounter;
    [SerializeField] private TextMeshProUGUI foodCounter;
    [SerializeField] private TextMeshProUGUI villagerCounter;


    void Update()
    {
        rockCounter.text = GameManager.Instance.totalRock.ToString();
        woodCounter.text = GameManager.Instance.totalWood.ToString();
        foodCounter.text = GameManager.Instance.totalFood.ToString();
        villagerCounter.text = GameManager.Instance.totalPopulation.ToString();
    }
}
