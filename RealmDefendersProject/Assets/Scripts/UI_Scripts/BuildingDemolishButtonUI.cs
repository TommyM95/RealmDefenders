using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButtonUI : MonoBehaviour
{
    [SerializeField] private Building building;

    private void Awake()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(()=> {
            so_BuildingType buildingType = building.GetComponent<BuildingTypeContainer>().buildingType;
            foreach (ResourceAmount resourceAmount in buildingType.buildResourceCostArray)
            {
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * .5f));
            }
            Destroy(building.gameObject);
        });
    }
}
