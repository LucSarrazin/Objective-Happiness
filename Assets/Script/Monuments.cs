using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monuments : MonoBehaviour
{
    [SerializeField] private int prosperityBoost;

    private void OnEnable()
    {
        GameManager.totalProgress += prosperityBoost;
    }
}
