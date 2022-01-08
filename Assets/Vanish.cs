using System.Collections;
using UnityEngine;

public class Vanish : Spells
{
    [SerializeField] Material _initialMaterial;
    [SerializeField] private Material stealthMaterial;
    private ParticleSystem _vanishSmoke;
    private float Duration { get; set; }

    private void Awake()
    {
        Name = "Vanish";
        Description = "The Rogue hides in the shadows. He cannot be seen.";
        Cooldown = 16f;
        Duration = 8f;
        _vanishSmoke = transform.Find("VanishSmoke").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        Timestamp = Time.time + Cooldown;
        StartCoroutine(nameof(ChangeMaterial));
        yield break;
    }

    private IEnumerator ChangeMaterial()
    {
        if(!_vanishSmoke.isPlaying)_vanishSmoke.Play(true);
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            r.material = stealthMaterial;
        }
        yield return new WaitForSeconds(Duration);
        foreach (Renderer r in rs)
            r.material = _initialMaterial;
    }
}
