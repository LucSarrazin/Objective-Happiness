using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private BuildingCosts buildingCosts;

    private float timeLeft = 0f;
    public int masonCount = 0;
    public bool isBuilding = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Villager" && other.gameObject.GetComponent<Villager>().type == Villager.types.mason)
            masonCount++;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Villager" && other.gameObject.GetComponent<Villager>().type == Villager.types.mason)
            masonCount--;
    }

    public void Update()
    {
        isBuilding = masonCount >= buildingCosts.requiredMason;
        if (isBuilding)
        {
            timeLeft += Time.deltaTime;
            if (timeLeft >= buildingCosts.buildTime)
            {
                Instantiate(buildingPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
