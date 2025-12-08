using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class BuildingCosts : ScriptableObject
{
    public int requiredMason;
    public int woodCost;
    public int rockCost;
    public float buildTime;

    public GameObject builderPrefab;
    public GameObject constructionSitePrefab;
    public GameObject buildingPrefab;
}
