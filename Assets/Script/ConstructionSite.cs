using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ConstructionSite : MonoBehaviour
{
    [SerializeField] public BuildingCosts buildingCosts;
    [SerializeField] private MonoBehaviour buildingScript;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Animator animator;
    private NavMeshObstacle navMeshObstacle;

    public bool isPreview = true;
    private float alpha = 0.5f;
    private bool canBuild = true;

    private float buildTime = 0f;
    public List<Villager> assignedMasons = new List<Villager>();
    public int masonCount = 0;
    public bool isBuilding = false;

    public void Awake()
    {
        if (buildingScript.enabled)
        {
            Destroy(this);
            isPreview = false;
            return;
        }

        renderer.material.SetColor("_ColorChange", new Color(1, 1, 1, alpha));
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        navMeshObstacle.enabled = false;
    }

    public void Place()
    {
        if (canBuild)
        {
            GameManager.Instance.totalWood -= buildingCosts.woodCost;
            GameManager.Instance.totalRock -= buildingCosts.rockCost;

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
            renderer.material.SetColor("_ColorChange", new Color(1, 0, 0, alpha));
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
            renderer.material.SetColor("_ColorChange", new Color(1, 1, 1, alpha));
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

        // float progress = buildTime / buildingCosts.buildTime * .6f + .2f; // starts at .2, jumps from .8 to 1 when finishing, for clear demarcation
        // renderer.material.SetColor("_ColorChange", new Color(1, 1, 1, progress));

        // while not constructed... so sad we couldn't get transparency to work properly in URP
        animator.Play("house wiggle");

        isBuilding = masonCount >= buildingCosts.requiredMason;
        if (isBuilding)
        {
            buildTime += Time.deltaTime;

            if (buildTime >= buildingCosts.buildTime)
            {
                animator.Play("idle");

                isBuilding = false;
                renderer.material.SetColor("_ColorChange", new Color(1, 1, 1, 1));
                buildingScript.enabled = true;
                navMeshObstacle.enabled = true;
                GameManager.Instance.ListBuildingInConstruction.Remove(gameObject);
                Destroy(this);
            }
        }
    }
}
