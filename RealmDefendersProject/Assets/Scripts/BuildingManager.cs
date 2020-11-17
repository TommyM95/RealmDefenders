using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public event EventHandler<OnActiveBuildingTypeChangeEventArgs> OnActiveBuildingTypeChange;
    /* To Send a args object through event system to gain access to the properties 
    in this case I want the sprite of the active building for previewing building*/
    public class OnActiveBuildingTypeChangeEventArgs : EventArgs
    {
        public so_BuildingType activeBuildingType;
    }

    private so_BuildingType activeBuildingType;
    private so_BuildingTypeList buildingTypeList;

    private Camera mainCamera;  // Main Camera of GameScene

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<so_BuildingTypeList>(typeof(so_BuildingTypeList).Name);
    }

    private void Start()
    {
        mainCamera = Camera.main;   // Caching Camera of the scene to save searching for the object multiple times saving preformance
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null)
            {
                Instantiate(activeBuildingType.prefab, UtilitieClass.GetMouseWorldPosition(), Quaternion.identity);
            }
        }

        //Debug.Log(GetMouseWorldPosition()); // Used for Testing the GetMouseWorldPosition Function
    }

    public void SetActiveBuildingType(so_BuildingType buildingType) //
    {
        activeBuildingType = buildingType;

        OnActiveBuildingTypeChange?.Invoke(this, new OnActiveBuildingTypeChangeEventArgs { 
            activeBuildingType = activeBuildingType
        });
    }

    public so_BuildingType GetActiveBuildingType()
    {
        return activeBuildingType;
    }
}
