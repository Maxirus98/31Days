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
        AbilityUtils.UpdateView(_playerCamera, _camera);
    }
}
