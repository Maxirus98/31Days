using UnityEngine;
using UnityEngine.UI;

public class RenderFromCamera : MonoBehaviour
{
    private Camera _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindWithTag("PlayerUiCamera").GetComponent<Camera>();
        _camera.forceIntoRenderTexture = true;
    }
}
