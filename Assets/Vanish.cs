using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Vanish : Spells
{
    private const string StealthLayerMask = "Stealth";

    [SerializeField] private Material initialMaterial;
    [SerializeField] private Material stealthMaterial;
    [SerializeField] private GameObject vanishSmoke;
    
    private BladeDance _bladeDance;
    private CharacterCombat _characterCombat;
    private string _initialLayerMask;
    private float Duration { get; set; }
    private float PreventStealthBreakTimeStamp { get; set; }
    private float _preventStealthBreakTime;
    private Renderer[] _renderers;
    private bool _isStealth;

    private void Awake()
    {
        Name = "Vanish";
        Description = "The Rogue hides in the shadows. He cannot be seen.";
        cooldown = 16f;
        Duration = 8f;
        _preventStealthBreakTime = 2f;
        _initialLayerMask = LayerMask.LayerToName(gameObject.layer);
        _characterCombat = GetComponent<CharacterCombat>();
        _bladeDance = GetComponent<BladeDance>();
        _renderers = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(nameof(DoSpell));
        }

        if (_characterCombat.CurrentCharacterCombatState.Equals(CharacterCombatState.InCombat) && _isStealth && Time.time > PreventStealthBreakTimeStamp)
        {
            Show();
        }
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + cooldown;
        Instantiate(vanishSmoke, transform.position, Quaternion.identity);
        _bladeDance.BuffDamage();
        StartCoroutine(nameof(UpdateStealth));
        yield break;
    }
    
    private void Show()
    {
        // change rogue layers so that enemies can follow the player.
        gameObject.layer = LayerMask.NameToLayer(_initialLayerMask);
        UseInitialMaterial();
        _isStealth = false;
        StartCoroutine(ShowCallback());
    }

    private IEnumerator ShowCallback()
    {
        yield return new WaitForSeconds(0.2f);
        _bladeDance.DebuffDamage();
    }

    private IEnumerator UpdateStealth()
    {
        Hide();
        yield return new WaitForSeconds(Duration);
        Show();
    }

    private void UseStealthMaterial()
    {
        foreach (Renderer r in _renderers)
        {
            r.material = stealthMaterial;
        }
    }

    private void UseInitialMaterial()
    {
        foreach (Renderer r in _renderers)
            r.material = initialMaterial;
    }

    private void Hide()
    {
        gameObject.layer = LayerMask.NameToLayer(StealthLayerMask);
        _isStealth = true;
        _characterCombat.UpdateCharacterCombatState(CharacterCombatState.OutCombat);
        UseStealthMaterial();
        PreventStealthBreakTimeStamp = Time.time + _preventStealthBreakTime;
    }
}
