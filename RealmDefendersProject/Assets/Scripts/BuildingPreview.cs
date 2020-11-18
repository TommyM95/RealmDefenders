using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{
    private GameObject spriteGameObject;
    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildManager_OnActiveBuildingTypeChanged;
    }

    private void BuildManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e)
    {
        if (e.activeBuildingType == null)
        {
            Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
        }
    }

    private void Update()
    {
        transform.position = UtilitieClass.GetMouseWorldPosition();
    }

    private void Show(Sprite previewSprite)
    {
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = previewSprite;
        spriteGameObject.SetActive(true);
        //spriteGameObject.GetComponent<SpriteRenderer>().color = new Color(255,0,0);
    }

    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}
