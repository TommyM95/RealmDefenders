using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipUI : MonoBehaviour
{
    public static ToolTipUI Instance { get; private set; }              // We are using this within a singleton pattern
    [SerializeField] private RectTransform canvasRectTransform;         // Referance to the Canvas's rect transform

    private RectTransform ParentRectTransform;                          // Referance to the parent objects rect transform
    private TextMeshProUGUI textMeshPro;                                // Referance to the text component of game object 
    private RectTransform textRectTransform;                            // Referance to text gameobject rect transform

    private RectTransform backgroundRectTransform;                      // Referance to the background image rect transform
    private Vector2 backgroundPadding;                                  // Background padding keeps the background image larger than the size / length of the text inside it

    private ToolTipTimer toolTipTimer;

    private void Awake()                                                // Setting all the referances
    {
        SetUp();
    }

    private void Update()
    {
        FollowMouse();

        if (toolTipTimer != null)
        {
            toolTipTimer.timer -= Time.deltaTime;
            if (toolTipTimer.timer < 0)
            {
                Hide();
            }
        }
    }

    private void FollowMouse()
    {
        // Moving the tool tip to the mouse position and ensuring the tool tip does not go beyond the right or upper edge of screen
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        ParentRectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetToolTipText(string toolTipText)
    {
        // This function Sets the text of the tool tip to what ever is passed in and updates the background size to fit around it
        textMeshPro.SetText(toolTipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundPadding *= 2;
        backgroundRectTransform.sizeDelta = textSize + backgroundPadding;
        backgroundRectTransform.ForceUpdateRectTransforms();
    }

    // Enable The ToolTip and Set the text
    public void Show(string toolTipText, ToolTipTimer toolTipTimer = null)
    {
        SetUp();
        this.toolTipTimer = toolTipTimer;
        gameObject.SetActive(true);
        SetToolTipText(toolTipText);
        FollowMouse();
    }

    // Disable the Tool Tip
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /* Set Referances (Function created to solve background exponentially growing after every time it was enable can move 
     * some ref's to be set in the awake that do not effect the scale of background image to save proforomance)
    */
    public void SetUp()
    {
        Instance = this;

        ParentRectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        textRectTransform = transform.Find("text").GetComponent<RectTransform>();
        backgroundPadding = new Vector2(textRectTransform.anchoredPosition.x, textRectTransform.anchoredPosition.y);

        Hide();
    }

    public class ToolTipTimer
    {
        public float timer;
    }

}
