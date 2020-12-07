using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private so_BuildingType buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishButton;
    private Transform buildingRepairButton;

    private void Awake()
    {
        buildingDemolishButton = transform.Find("pf_BuildingDemolishButton");
        buildingRepairButton = transform.Find("pf_BuildingRepairButton");
        HideDemolishButton();
        HideRepairButton();
    }

    private void Start()
    {
        buildingType = GetComponent<BuildingTypeContainer>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetMaxHealth(buildingType.maxHealth, true);
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamageTaken += HealthSystem_OnDamageTaken;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        if (healthSystem.IsFullHealth())
        {
            HideRepairButton();
        }
    }

    private void HealthSystem_OnDamageTaken(object sender, EventArgs e)
    {
        ShowRepairButton();
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

    private void ShowRepairButton()
    {
        if (buildingRepairButton != null)
        {
            buildingRepairButton.gameObject.SetActive(true);
        }
    }

    private void HideRepairButton()
    {
        if (buildingRepairButton != null)
        {
            buildingRepairButton.gameObject.SetActive(false);
        }
    }
}
