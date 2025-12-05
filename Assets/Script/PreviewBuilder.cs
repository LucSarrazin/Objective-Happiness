using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PreviewBuilder : MonoBehaviour
{
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private BuildingCosts buildingCosts;

    private SpriteRenderer spriteRenderer;
    private float alpha = 0.5f;
    private bool canBuild = true;

    public void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, alpha);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            spriteRenderer.color = new Color(1, 0, 0, alpha);
            canBuild = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            canBuild = true;
        }
    }

    public void OnDestroy()
    {
        if (canBuild)
        {
            GameObject placedBuilding = Instantiate(buildingPrefab, transform.position, Quaternion.identity);

            GameManager.totalWood -= buildingCosts.woodCost;
            GameManager.totalRock -= buildingCosts.rockCost;

            SpriteRenderer placedSpriteRenderer = placedBuilding.GetComponent<SpriteRenderer>();
            Destroy(gameObject);
        }
    }
}
