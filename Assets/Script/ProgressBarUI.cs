using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private float maxValue;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        slider.value = GameManager.Instance.totalProgress / GameManager.requieredProgress * maxValue;
    }
}
