using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2DArry = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

        int nearResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArry)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearResourceAmount++;
                }

            }
        }

        nearResourceAmount = Mathf.Clamp(nearResourceAmount, 0, resourceGeneratorData.maxResouceAmount);

        return nearResourceAmount;
    }
    private float timer;
    private float timerMax;
    private ResourceGeneratorData resourceGeneratorData;

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeContainer>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        int nearResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);

        if (nearResourceAmount == 0)
        {
            //The there are no resource nodes nearby
            enabled = false; // So we disable the resource generator
        }
        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) + resourceGeneratorData.timerMax * 
                (1 - (float)nearResourceAmount / resourceGeneratorData.maxResouceAmount);
        }
        //Debug.Log("nearResourceAmount: " + nearResourceAmount + " timerMax: " + timerMax);

    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timerMax;
            //Debug.Log("Adding : " + buildingType.resourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }

    public float GetNormalizedTimer()
    {
        return timer / timerMax;
    }

    public float GetResourcesGeneratedPerSeccond()
    {
        return 1 / timerMax;
    }
}
