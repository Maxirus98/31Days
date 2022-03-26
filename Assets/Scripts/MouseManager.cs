using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3>
{
    //ICI : Vector3 est le paramètre pour le handler event qui va se faire appeler
    //1. déFINIR LA CLASSE D'ÉVÉNEMENT avec les param qu'on veut passer
}

// TODO: Should be separated into 2 distincts scripts, one for combat and one for interacting with anything in the scene.
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
                        var spellSpawnArea = spell.spawnArea;
                        if (spellSpawnArea)
                        {
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

            if (Input.GetMouseButtonDown(0))
            {
                Interactable interactable;
                switch (hit.collider.tag)
                {
                    // bad way to put a targeter under the enemy
                    case "Enemy":
                        interactable = hit.collider.GetComponent<Interactable>();
                        if (interactable)
                        {
                            SetFocus(interactable);
                        }
                        break;
                    case "Shop":
                        interactable = hit.collider.GetComponent<Interactable>();
                        if (interactable)
                        {
                            SetFocus(interactable);
                        }
                        break;
                    default:
                        SetFocus(null);
                        break;
                }
                
                //3e étape
                OnClickEnvironment.Invoke(hit.point);
            }
        }
    }
    
    public void SetFocus(Enemy newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
        }
        if (newFocus != null)
            newFocus.OnFocused();
    }

    public void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
        }
        if (newFocus != null)
            newFocus.OnFocused();
    }
}
