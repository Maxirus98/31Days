using System.Collections;
using UnityEngine;

public class Vanish : Spells
{
    [SerializeField] Material _initialMaterial;
    [SerializeField] private Material stealthMaterial;
    private ParticleSystem _vanishSmoke;
    private BladeDance _bladeDance;
    private CharacterCombat _characterCombat;
    private float Duration { get; set; }
    private Renderer[] _renderers;
    private bool isStealth;

    private void Awake()
    {
        Name = "Vanish";
        Description = "The Rogue hides in the shadows. He cannot be seen.";
        cooldown = 16f;
        Duration = 8f;
        _vanishSmoke = transform.Find("VanishSmoke").GetComponent<ParticleSystem>();
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

        if (_characterCombat.CurrentCharacterState.Equals(CharacterState.InCombat) && isStealth)
        {
            Show();
        }
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + cooldown;
        _bladeDance.BuffDamage();
        StartCoroutine(nameof(UpdateStealth));
        yield break;
    }
    
    private void Show()
    {
        Debug.Log("show called");
        // change rogue layers so that enemies can follow the player.
        UseInitialMaterial();
        isStealth = false;
    }

    private IEnumerator UpdateStealth()
    {
        if(!_vanishSmoke.isPlaying)_vanishSmoke.Play(true);
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
            r.material = _initialMaterial;
    }

    private void Hide()
    {
        Debug.Log("hide called");
        isStealth = true;
        _characterCombat.UpdateCharacterState(CharacterState.OutCombat);
        UseStealthMaterial();
        // change rogue layers so that enemies can't follow the player.
        // prevent attacks from breaking stealth for 1 - 2 sec.
    }
}
