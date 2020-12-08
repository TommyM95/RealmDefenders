using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButtonUI : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private so_ResourceType gemResourceType;

    private void Awake()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(()=> {
            float missingHealth = healthSystem.GetMaxHealthAmount() - healthSystem.GetCurrentHealthAmount();
            int repairCost = (int)missingHealth / 2;

            ResourceAmount[] resourceCost = new ResourceAmount[] { new ResourceAmount {
                resourceType = gemResourceType, amount = repairCost } };

            if (ResourceManager.Instance.CanAfford(resourceCost))
            {
                ResourceManager.Instance.SpendResource(resourceCost);
                healthSystem.HealFull();
            }
            else
            {
                // Cant afford so do nothing
                ToolTipUI.Instance.Show("You don't have enough Gems!", new ToolTipUI.ToolTipTimer { timer = 2f});
            }
        });
    }
}
