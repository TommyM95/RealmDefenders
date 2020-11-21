using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuildingTypeUI : MonoBehaviour
{
    [SerializeField] private Sprite mouseSprite;
    [SerializeField] private List<so_BuildingType> ignoreBuildingTypeList;

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
        arrowButton.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -50);    // Change Size of cursor sprite representation

        arrowButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        MouseEnterExitEvent mouseEnterExitEvent = arrowButton.GetComponent<MouseEnterExitEvent>();
        mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) => {
            ToolTipUI.Instance.Show("Cursor");
        };
        mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) => {
            ToolTipUI.Instance.Hide();
        };

        index++;


        foreach (so_BuildingType buildingType in buildingTypeList.list)
        {
            if (ignoreBuildingTypeList.Contains(buildingType)) continue;
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            offset = +117f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);

            buttonTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            mouseEnterExitEvent = buttonTransform.GetComponent<MouseEnterExitEvent>();
            mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>{
                ToolTipUI.Instance.Show(buildingType.nameString + "\n" + 
                    "Cost's " + buildingType.GetCostOfBuildingAsString()+"");
            };
            mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) => {
                ToolTipUI.Instance.Hide();
            };

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
