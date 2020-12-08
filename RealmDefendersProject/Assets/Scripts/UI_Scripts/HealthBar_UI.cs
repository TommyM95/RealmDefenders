using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar_UI : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTransform;

    private void Awake()
    {
        barTransform = transform.Find("bar");
    }

    private void Start()
    {
        healthSystem.OnDamageTaken += HealthSystem_OnDamageTaken;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        UpdateBar();
        HealthBarVisable();
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        UpdateBar();
        HealthBarVisable();
    }

    private void HealthSystem_OnDamageTaken(object sender, EventArgs e)
    {
        UpdateBar();
        HealthBarVisable();
    }

    private void UpdateBar()
    {
        // Update the healthbar scale by the normalized healthvalue
        barTransform.localScale = new Vector3(healthSystem.GetCurrentHealthAmountNormalized(),1,1);
    }

    private void HealthBarVisable()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
