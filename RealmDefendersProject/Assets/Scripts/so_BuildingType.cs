using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building Type")]
public class so_BuildingType : ScriptableObject
{
    public string nameString;                               // Name of building
    public Transform prefab;                                // Prefab of the complete building
    public ResourceGeneratorData resourceGeneratorData;     // ResourceGeneratorData Custom class to define how much resources is generated persecond 
}
