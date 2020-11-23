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

    [SerializeField] private Building playerCampBuilding;
    private so_BuildingType activeBuildingType;
    private so_BuildingTypeList buildingTypeList;
    [SerializeField] float maxDistanceBetweenBuildingRadius;
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
            if (activeBuildingType != null) {
                if (CanBuildingSpawn(activeBuildingType, UtilitieClass.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.buildResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResource(activeBuildingType.buildResourceCostArray);
                        Instantiate(activeBuildingType.prefab, UtilitieClass.GetMouseWorldPosition(), Quaternion.identity);
                    }
                    else
                    {
                        ToolTipUI.Instance.Show("Not enough resources ", new ToolTipUI.ToolTipTimer { timer = 2f });
                    }
                }
                else
                {
                    ToolTipUI.Instance.Show(errorMessage, new ToolTipUI.ToolTipTimer { timer = 2f });
                }
            }
        }

        //Debug.Log(GetMouseWorldPosition()); // Used for Testing the GetMouseWorldPosition Function
        /*if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 enemySpawnPos = UtilitieClass.GetMouseWorldPosition() + UtilitieClass.GetRandomDirection() * 5f;
            Enemy.Create(enemySpawnPos);
        }*/
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

    private bool CanBuildingSpawn(so_BuildingType buildingType, Vector3 position, out string errorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isPlacementAreaClear = collider2DArray.Length == 0;
        if (!isPlacementAreaClear)
        {
            errorMessage = "Area is not clear";
            return false;
        }

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minDistanceBetweenBuildingRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeContainer buildingTypeContainer = collider2D.GetComponent<BuildingTypeContainer>();
            Debug.Log(buildingTypeContainer.buildingType);
            if (buildingTypeContainer.buildingType == buildingType)
            {
                errorMessage = "Too close to another building of the same type";
                // Already building of type within placement radius
                return false;
            }
        }

         // Set max distance building is placeable on map based on how far away it is from "other building"
         collider2DArray = Physics2D.OverlapCircleAll(position, maxDistanceBetweenBuildingRadius);
         foreach (Collider2D collider2D in collider2DArray)
         {
             BuildingTypeContainer buildingTypeContainer = collider2D.GetComponent<BuildingTypeContainer>();
             if (buildingTypeContainer != null)
             {
                errorMessage = null;
                 return true;
             }
         }

        errorMessage = "Too far from other buildings!";
         return false;
        
        //return true; //temp
    }

    public Building GetPlayerCampBuilding()
    {
        return playerCampBuilding;
    }
}
