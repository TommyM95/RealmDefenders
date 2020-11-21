using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Resource Type")]
public class so_ResourceType : ScriptableObject
{
    public string nameString;
    public string nameShort;
    public Sprite uiSprite;
    public string colourHex;
}
