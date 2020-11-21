using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceGeneratorData
{
    public float timerMax;                  // To Set the timer to disired amount on reset
    public so_ResourceType resourceType;    // Used to define what resource type is being generataed by the building
    public float resourceDetectionRadius;   // To Determin the radius for detecting resources
    public int maxResouceAmount;            // used to determine the max amount of resource nodes near a gather that have an effect of the gather speed
}
