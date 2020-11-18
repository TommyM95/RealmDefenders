using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGatheringOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform barTransform;

    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        barTransform = transform.Find("bar");
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.uiSprite;
        transform.Find("bar").localScale = new Vector3(resourceGenerator.GetNormalizedTimer(), 1, 1);
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetResourcesGeneratedPerSeccond().ToString("F1"));
    }

    private void Update()
    {
        barTransform.localScale = new Vector3(1 - resourceGenerator.GetNormalizedTimer(), 1, 1);
    }
}
