using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    private Canvas _canvas;
    private Camera _camera;
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _camera = _canvas.worldCamera;
    }

    void Update()
    {
        // TODO: Refactor for an event instead of in the Update method?
        // Activate the canvas only when the camera is on.
        _canvas.enabled = _camera.isActiveAndEnabled;
    }
}
