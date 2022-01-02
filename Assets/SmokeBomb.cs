using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : Spells
{
    [SerializeField] private GameObject smokeBomb;
    private float Duration { get; set; }

    private void Awake()
    {
        Name = "Vanish";
        Description = "The Rogue hides in the shadows. He cannot be seen.";
        Cooldown = 1f;
        Duration = 8f;
    }

    private void Update()
    {
        if (Time.time > Timestamp && Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(nameof(DoSpell));
        }
    }

    protected override IEnumerator DoSpell()
    {
        var cloneSmokeBomb = Instantiate(smokeBomb, transform.position, transform.rotation);
        yield return new WaitForSeconds(Duration);
        Destroy(cloneSmokeBomb);
    }
}
