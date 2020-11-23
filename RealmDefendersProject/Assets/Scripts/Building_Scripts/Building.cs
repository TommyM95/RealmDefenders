using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private so_BuildingType buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishButton;

    private void Awake()
    {
        buildingDemolishButton = transform.Find("pf_BuildingDemolishButton");
        HideDemolishButton();
    }

    private void Start()
    {
        buildingType = GetComponent<BuildingTypeContainer>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetMaxHealth(buildingType.maxHealth, true);
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        ShowDemolishButton();
    }

    private void OnMouseExit()
    {
        HideDemolishButton();
    }

    private void ShowDemolishButton()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(true);
        }
    }

    private void HideDemolishButton()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(false);
        }
    }
}
