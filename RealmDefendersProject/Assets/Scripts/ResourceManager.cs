using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }    // using a singleton patter, need access to this so public get private set

    public event EventHandler OnResourceAmountChange;

    private Dictionary<so_ResourceType, int> resourceAmountDictionary;  // Using the dictionary data structure to store the amount of resources player has

    private void Awake()
    {
        Instance = this;

        resourceAmountDictionary = new Dictionary<so_ResourceType, int>();

        so_ResourceTypeList resourceTypeList = Resources.Load<so_ResourceTypeList>(typeof(so_ResourceTypeList).Name);

        // Cycle through each item in list and initalise to 0
        foreach (so_ResourceType resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }

        TestLogResouceAmountDictionary();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            so_ResourceTypeList resourceTypeList = Resources.Load<so_ResourceTypeList>(typeof(so_ResourceTypeList).Name);
            //AddResource(resourceTypeList.list[0], 5);
            //TestLogResouceAmountDictionary();
        }
    }

    private void TestLogResouceAmountDictionary()   //Test
    {
        foreach (so_ResourceType resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(so_ResourceType resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;   // Add the amount var to the defined resource type in the dictionary

        OnResourceAmountChange?.Invoke(this, EventArgs.Empty); //   Check if something is listening for event if yes trigger event if no saves null ref exception

        TestLogResouceAmountDictionary();
    }

    public int GetResourceAmount(so_ResourceType resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }
}
