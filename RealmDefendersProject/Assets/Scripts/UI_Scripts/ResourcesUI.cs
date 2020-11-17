using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    private so_ResourceTypeList resourceTypeList;
    private Dictionary<so_ResourceType, Transform> resourceTypeTransformDictionary;

    private void Awake()
    {
        resourceTypeList = Resources.Load<so_ResourceTypeList>(typeof(so_ResourceTypeList).Name);

        resourceTypeTransformDictionary = new Dictionary<so_ResourceType, Transform>();
        Transform resourceTemplate = transform.Find("resourceTemplate");
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        foreach (so_ResourceType resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            float offset = -234f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * index, 0);
            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.uiSprite;

            resourceTypeTransformDictionary[resourceType] = resourceTransform;
            index++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChange += ResourceManager_OnResourceChange;
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceChange(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (so_ResourceType resourceType in resourceTypeList.list)
        {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);

            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];

            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
