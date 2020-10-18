using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building Type")]
public class so_BuildingType : ScriptableObject
{
    public string nameString;
    public Transform prefab;
}
