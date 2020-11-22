﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building Type")]
public class so_BuildingType : ScriptableObject
{
    public string nameString;                               // Name of building
    public Transform prefab;                                // Prefab of the complete building
    public ResourceGeneratorData resourceGeneratorData;     // ResourceGeneratorData Custom class to define how much resources is generated persecond
    public Sprite sprite;                                   // Building UI Sprite
    public float minDistanceBetweenBuildingRadius;          // Used to determine the minimum distance between buildings that can be placed
    public ResourceAmount[] buildResourceCostArray;         // Arry because we might have more than one resource needed to build a building
    public float maxHealth;                                 // Maximum Health of building

    public string GetCostOfBuildingAsString()
    {
        string str = "";
        foreach (ResourceAmount resourceAmount in buildResourceCostArray)
        {
            
               str += resourceAmount.resourceType.nameString + ": " + resourceAmount.amount + " ";
        }
        return str;
    }
}
