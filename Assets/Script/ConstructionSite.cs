using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ConstructionSite : MonoBehaviour
{
    [SerializeField] private BuildingCosts buildingCosts;
    [SerializeField] private MonoBehaviour buildingScript;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private NavMeshObstacle navMeshObstacle;

    public bool isPreview = true;
    private float alpha = 0.5f;
    private bool canBuild = true;

    private float buildTime = 0f;
    private List<Villager> assignedMasons = new List<Villager>();
    public int masonCount = 0;
    public bool isBuilding = false;

    public void Awake()
    {
        if (buildingScript.enabled)
        {
            Destroy(this);
            return;
        }

        spriteRenderer.color = new Color(1, 1, 1, alpha);
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;
    }

    public void Place()
    {
        if (canBuild)
        {
            GameManager.totalWood -= buildingCosts.woodCost;
            GameManager.totalRock -= buildingCosts.rockCost;

            isPreview = false;
            GameManager.Instance.ListBuildingInConstruction.Add(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void OnTriggerStay(Collider other)
    {
        if (isPreview && other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            spriteRenderer.color = new Color(1, 0, 0, alpha);
            canBuild = false;
        }
        else if (other.gameObject.tag == "Villager")
        {
            Villager villager = other.gameObject.GetComponent<Villager>();
            if (villager.type == Villager.types.mason && !assignedMasons.Contains(villager) && masonCount < buildingCosts.requiredMason)
            {
                assignedMasons.Add(villager);
                masonCount++;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (isPreview && other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            canBuild = true;
        }
        else if (other.gameObject.tag == "Villager")
        {
            Villager villager = other.gameObject.GetComponent<Villager>();
            if (villager.type == Villager.types.mason && assignedMasons.Contains(villager))
            {
                assignedMasons.Remove(villager);
                masonCount--;
            }
        }
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
                isBuilding = false;
                spriteRenderer.color = new Color(1, 1, 1, 1);
                buildingScript.enabled = true;
                navMeshObstacle.enabled = true;
                GameManager.Instance.ListBuildingInConstruction.Remove(gameObject);
                Destroy(this);
            }
        }
    }
}
