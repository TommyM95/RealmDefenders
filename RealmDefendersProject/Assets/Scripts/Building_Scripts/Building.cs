using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private so_BuildingType buildingType;
    private HealthSystem healthSystem;

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
}
