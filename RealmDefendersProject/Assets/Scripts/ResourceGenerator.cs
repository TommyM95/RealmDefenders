using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float timer;
    private float timerMax;
    private so_BuildingType buildingType;

    private void Awake()
    {
        buildingType = GetComponent<BuildingTypeContainer>().buildingType;
        timerMax = buildingType.resourceGeneratorData.timerMax;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timerMax;
            //Debug.Log("Adding : " + buildingType.resourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType, 1);
        }
    }
}
