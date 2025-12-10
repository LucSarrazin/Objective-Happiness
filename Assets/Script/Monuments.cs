using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monuments : MonoBehaviour
{
    [SerializeField] private float prosperityBoost;

    private void OnEnable()
    {
        
        if (GameManager.Instance == null)
        {
            GameManager.Instance = FindObjectOfType<GameManager>();
        }
        
        GameManager.Instance.totalProgress += prosperityBoost;
    }
}
