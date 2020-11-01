using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Insance { get; private set; }
    private so_BuildingType activeBuildingType;
    private so_BuildingTypeList buildingTypeList;

    private Camera mainCamera;  // Main Camera of GameScene

    private void Awake()
    {
        Insance = this;
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
                Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            activeBuildingType = buildingTypeList.list[0];
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            activeBuildingType = buildingTypeList.list[1];
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

    public void SetActiveBuildingType(so_BuildingType buildingType) //
    {
        activeBuildingType = buildingType;
    }

    public so_BuildingType GetActiveBuildingType()
    {
        return activeBuildingType;
    }
}
