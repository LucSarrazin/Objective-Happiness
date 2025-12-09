using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    [SerializeField] private BuildingCosts buildingCosts;
    [SerializeField] private MonoBehaviour buildingScript;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isPreview = true;
    private float alpha = 0.5f;
    private bool canBuild = true;

    private float buildTime = 0f;
    public int masonCount = 0;
    public bool isBuilding = false;

    public void Awake()
    {
        if (buildingScript.enabled)
            Destroy(this);
        else
            spriteRenderer.color = new Color(1, 1, 1, alpha);
    }

    public void Place()
    {
        if (canBuild)
        {
            GameManager.totalWood -= buildingCosts.woodCost;
            GameManager.totalRock -= buildingCosts.rockCost;

            isPreview = false;
            GameManager.ListBuildingInConstruction.Add(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isPreview && other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            spriteRenderer.color = new Color(1, 0, 0, alpha);
            canBuild = false;
        }
        else if (other.gameObject.tag == "Villager" && other.gameObject.GetComponent<Villager>().type == Villager.types.mason)
            masonCount++;
    }

    public void OnTriggerExit(Collider other)
    {
        if (isPreview && other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            canBuild = true;
        }
        else if (other.gameObject.tag == "Villager" && other.gameObject.GetComponent<Villager>().type == Villager.types.mason)
            masonCount--;
    }

    public void Update()
    {
        if (isPreview)
            return;

        spriteRenderer.color = new Color(1, 1, 1, buildTime / buildingCosts.buildTime * .6f + .2f);  // starts at .2, jumps from .8 to 1 when finishing, for clear demarcation

        isBuilding = masonCount >= buildingCosts.requiredMason;
        if (isBuilding)
        {
            buildTime += Time.deltaTime;

            if (buildTime >= buildingCosts.buildTime)
            {
                spriteRenderer.color = new Color(1, 1, 1, 1);
                buildingScript.enabled = true;
                GameManager.ListBuildingInConstruction.Remove(gameObject);
                Destroy(this);
            }
        }
    }
}
