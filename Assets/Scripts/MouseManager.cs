using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3>
{
    //ICI : Vector3 est le paramètre pour le handler event qui va se faire appeler
    //1. déFINIR LA CLASSE D'ÉVÉNEMENT avec les param qu'on veut passer
}


public class MouseManager : MonoBehaviour
{
    [SerializeField] private LayerMask _clickableLayer;
    [SerializeField] private LayerMask _attackableLayer;
    
    [SerializeField] private Texture2D _target;
    [SerializeField] private Texture2D _pointer;
    [SerializeField] private Texture2D _shop;
    private Transform _targeter;
    private Animator _playerAnimator;
    [SerializeField] public Interactable focus;
    private Camera mainCamera;

    //2e étape, déclarer le handler
    public EventVector3 OnClickEnvironment;

    private void Start()
    {
        _playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 25, _clickableLayer.value))
        {
            switch (hit.collider.tag)
            {
                case "Shop":
                    Cursor.SetCursor(_shop, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(_target, new Vector2(16, 16), CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(_pointer, new Vector2(16, 16), CursorMode.Auto);
                    break;
                    ;
            }

            // target is not good
            if (Input.GetMouseButtonDown(0))
            {
                switch (hit.collider.tag)
                {
                    // horrible way to put a targeter under the enemy
                    case "Enemy":
                        Interactable interactable = hit.collider.GetComponent<Interactable>();
                        Cursor.SetCursor(_target, new Vector2(16, 16), CursorMode.Auto);
                        _targeter = hit.collider.transform.GetChild(hit.collider.transform.childCount - 1);
                        _targeter.gameObject.SetActive(true);
                        if (interactable)
                        {
                            SetFocus(interactable);
                        }
                        break;
                    default:
                        if(_targeter) _targeter.gameObject.SetActive(false);
                        if (focus) focus = null;
                        break;
                }
                OnClickEnvironment.Invoke(hit.point);
            }
        }
    }
    
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

}
