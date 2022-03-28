using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    #region string access region
    private readonly string TOOLTIP_BACKGROUND = "Background";
    private readonly string TOOLTIP_TEXT = "Text";
    private readonly string PLAYER_CAMERA = "PlayerCamera";
    #endregion
    
    private TextMeshProUGUI _tooltipText;
    private RectTransform _backgroundRectTransform;
    private Camera _playerCamera;
    
    protected void Awake()
    {
        _backgroundRectTransform = transform.Find(TOOLTIP_BACKGROUND).GetComponent<RectTransform>();
        _tooltipText = transform.Find(TOOLTIP_TEXT).GetComponent<TextMeshProUGUI>();
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

    private void MoveTooltipToMouse()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, _playerCamera, out Vector2 localPoint);
        transform.localPosition = localPoint;
    }

    public void Show(string content)
    {
        _tooltipText.text = content;
        var textPaddingSize = 2f;
        Vector2 backgroundSize = new Vector2(_tooltipText.preferredWidth + textPaddingSize, _tooltipText.preferredHeight + textPaddingSize);
        _backgroundRectTransform.sizeDelta = backgroundSize;
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
