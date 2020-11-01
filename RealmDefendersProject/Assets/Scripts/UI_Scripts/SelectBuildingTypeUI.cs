using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuildingTypeUI : MonoBehaviour
{
    [SerializeField] private Sprite mouseSprite;

    private Dictionary<so_BuildingType, Transform> buttonTransformDictionary;
    private Transform arrowButton;

    private void Awake()
    {
        Transform buttonTemplate = transform.Find("ButtonTemplate");
        buttonTemplate.gameObject.SetActive(false);

        so_BuildingTypeList buildingTypeList = Resources.Load<so_BuildingTypeList>(typeof(so_BuildingTypeList).Name);

        buttonTransformDictionary = new Dictionary<so_BuildingType, Transform>();
        
        int index = 0;

        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);

        float offset = +117f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);

        arrowButton.Find("Image").GetComponent<Image>().sprite = mouseSprite;
        arrowButton.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        arrowButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;

        
        foreach (so_BuildingType buildingType in buildingTypeList.list)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            offset = +117f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);

            buttonTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            buttonTransformDictionary[buildingType] = buttonTransform;

            index++;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
        UpdateActiveBuildingType();
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e)
    {
        UpdateActiveBuildingType();
    }

    private void UpdateActiveBuildingType()
    {
        arrowButton.Find("selected").gameObject.SetActive(false);
        foreach (so_BuildingType buildingType in buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("selected").gameObject.SetActive(false);
        }

        so_BuildingType activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null)
        {
            arrowButton.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            buttonTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
        
    }
}
