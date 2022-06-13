using UnityEngine;

/// <summary>
/// Add the current camera to the canvas component
/// This Script is on the AmnesiaStoreUI GameObject 
/// </summary>
[RequireComponent(typeof(Canvas))]
public class ToggleCanvas : MonoBehaviour
{
    private Canvas _canvas;
    private Camera _camera;
    
    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _camera = _canvas.worldCamera;
    }

    private void Update()
    {
        // TODO: Refactor for an event instead of in the Update method?
        _canvas.enabled = _camera.isActiveAndEnabled;
    }
}
