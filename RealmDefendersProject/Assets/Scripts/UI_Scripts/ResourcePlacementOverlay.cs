using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcePlacementOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    private Transform resourcePlacementTransform;

    private void Awake()
    {
        resourcePlacementTransform = this.transform.Find("text");
        Hide();
    }
    private void Update()
    {
        int nearByResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        float percent = Mathf.RoundToInt((float)nearByResourceAmount / resourceGeneratorData.maxResouceAmount * 100f);
        resourcePlacementTransform.GetComponent<TextMeshPro>().SetText("%" + percent);
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.uiSprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    
}
