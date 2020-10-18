﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform pf_WoodHarvester = null;

    private Camera mainCamera;  // Main Camera of GameScene

    private void Start()
    {
        
        mainCamera = Camera.main;   // Caching Camera of the scene to save searching for the object multiple times saving preformance
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(pf_WoodHarvester, GetMouseWorldPosition(), Quaternion.identity);
        }

        //Debug.Log(GetMouseWorldPosition()); // Used for Testing the GetMouseWorldPosition Function
    }

    private Vector3 GetMouseWorldPosition() // This Function Returns the World Position of the Mouse
    {
        // Get Screen Position of the Mouse and Convert it to World Position, Set Z axis to 0
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
