using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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
    [SerializeField] public Interactable focus;
    private Camera mainCamera;
    private GameObject _player;
    private SkillShotSpell[] _skillShotSpells;

    //2e étape, déclarer le handler
    public EventVector3 OnClickEnvironment;

    private void Start()
    {
        mainCamera = Camera.main;
        _player = GameObject.FindWithTag("Player");
        _skillShotSpells = _player.GetComponents<SkillShotSpell>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, _clickableLayer.value))
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
                    // foreach must be costly here. Try finding another way to spawn on mouse position.
                    foreach (var spell in _skillShotSpells)
                    {
                        Debug.Log(spell.MaxRange);
                        var spellSpawnArea = spell.spawnArea;
                        if (spellSpawnArea)
                        {
                            Debug.Log("dist "  + Vector3.Distance(spellSpawnArea.transform.position, _player.transform.position));
                            if (Vector3.Distance(spellSpawnArea.transform.position, _player.transform.position) <
                                spell.MaxRange)
                            {
                                spellSpawnArea.transform.position = hit.point + Vector3.up;
                            }
                            else
                            {
                                Vector3 playerToCursor = hit.point - _player.transform.position;
                                Vector3 dir = playerToCursor.normalized;
                                Vector3 cursorVector = dir * spell.MaxRange;
                                Vector3 finalPos = _player.transform.position + cursorVector;
                                finalPos.y = hit.point.y + 1;
                                spellSpawnArea.transform.position = finalPos;
                            }
                        }
                    }
                    
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
