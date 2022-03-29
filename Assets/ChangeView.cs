using UnityEngine;

public class ChangeView : Interactable
{
    [SerializeField] private Camera _camera;
    
    private readonly string PLAYER_CAMERA = "PlayerCamera";
    private Camera _playerCamera;
    
    public override void Start()
    {
        base.Start();
        _playerCamera = GameObject.Find(PLAYER_CAMERA).GetComponent<Camera>();
    }
    
    // Set Target here
    public override void OnFocused()
    {
        base.OnFocused();
        AbilityUtils.UpdateView(_playerCamera, _camera);
    }
}
