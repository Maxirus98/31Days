﻿using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    #region string access region
    private readonly string TOOLTIP_BACKGROUND = "Background";
    private readonly string TOOLTIP_TITLE = "Title";
    private readonly string TOOLTIP_TEXT = "Description";
    private readonly string PLAYER_CAMERA = "PlayerCamera";
    #endregion

    private static Tooltip _instance;
    private RectTransform _rectTransform;
    private TextMeshProUGUI _tooltipText;
    private TextMeshProUGUI _tooltipTitle;
    private RectTransform _backgroundRectTransform;
    private Camera _playerCamera;
    
    protected  void Awake()
    {
        _instance = this;
        _tooltipTitle = transform.Find(TOOLTIP_TITLE).GetComponent<TextMeshProUGUI>();
        _tooltipText = transform.Find(TOOLTIP_TEXT).GetComponent<TextMeshProUGUI>();
        _rectTransform = transform.parent.GetComponent<RectTransform>();
        _backgroundRectTransform = transform.Find(TOOLTIP_BACKGROUND).GetComponent<RectTransform>();
    }

    private void Start()
    {
        _playerCamera = GameObject.Find(PLAYER_CAMERA).GetComponent<Camera>();
        Hide();
    }

    private void Update()
    {
        MoveTooltipToMouse();
    }
    
    /// <summary>
    /// Static method of singleton Tooltip to show the tooltip with the correct content
    /// </summary>
    /// <param name="title">Header of the tooltip</param>
    /// <param name="description">Content of the tooltip</param>
    public static void ShowTooltip(string title, string description)
    {
        _instance.Show(title, description);
    }
    
    public static void HideTooltip()
    {
        _instance.Hide();
    }

    private void MoveTooltipToMouse()
    {
        var mousePosition = new Vector2(Input.mousePosition.x +_backgroundRectTransform.sizeDelta.x, Input.mousePosition.y);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, mousePosition, _playerCamera, out var localPoint);
        transform.localPosition = localPoint;
    }

    private void Show(string title, string description)
    {
        if (string.IsNullOrEmpty(description))
            return;
        _tooltipTitle.text = title;
        _tooltipText.text = description;
        var textPaddingSize = 2f;
        // TODO: Fix tooltip
        var backgroundSize = new Vector2(_tooltipText.renderedWidth + textPaddingSize, _tooltipText.renderedWidth + textPaddingSize);
        _backgroundRectTransform.sizeDelta = backgroundSize;
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
