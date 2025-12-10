using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monuments : MonoBehaviour
{
    [SerializeField] private float prosperityBoost;

    private void OnEnable()
    {
        GameManager.Instance.totalProgress += prosperityBoost;
    }
}
