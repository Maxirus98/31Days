using UnityEngine;

public class ChangeView : Interactable
{
    [SerializeField] private Camera _camera;
    private Camera _playerCamera;
    
    public override void Start()
    {
        base.Start();
        _playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
    }
    
    // Set Target here
    public override void OnFocused()
    {
        base.OnFocused();
        UpdateView();
    }
    
    // Remove target here
    public override void OnDefocused()
    {
        base.OnDefocused();
        EnableMainCamera();
    }

    private void UpdateView()
    {
        if (_camera == null)
        {
            Debug.LogWarning($"No Camera to render." +
                             $" This is probably due that you forgot to add it to the script ChangeView of {gameObject.name}" );
        }
        _playerCamera.enabled = false;
        _camera.enabled = true;
        print($"call {_playerCamera.enabled}");
        print($"call {_camera.enabled}");

    }
    
    private void EnableMainCamera()
    {
        _playerCamera.enabled = true;
        _camera.enabled = false;
    }
}
