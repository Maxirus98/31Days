using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spells : MonoBehaviour
{
    protected CharacterCombat CharacterCombat;
    public delegate void AttackCallback(CharacterCombatState characterCombatState);
    public AttackCallback attackCallback;
    protected string Name { get; set; }
    protected string Description { get; set; }
    public float cooldown;
    public float Timestamp { get; set; }

    public float BaseDamage { get; set; }
    protected bool IsAutoTarget { get; set; }
    protected float Range { get; set; }
    public float hitRadius = 1f;

    [SerializeField] protected LayerMask enemyLayers;
    [SerializeField] protected PlayerAnimator _playerAnimator;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int spellSlot;
    
    // The slot it will be placed into in the Spellbar UI.
    
    protected Animator animator;
    
    protected virtual void Start()
    {
        CharacterCombat = GetComponent<CharacterCombat>();
        attackCallback = CharacterCombat.UpdateCharacterCombatState;
        _playerAnimator = GetComponent<PlayerAnimator>();
        animator = GetComponent<Animator>();
        StartCoroutine(initSpellSprites());
    }

    // TODO: Find a better way than waiting
    private IEnumerator initSpellSprites()
    {
        yield return new WaitForSeconds(1f);
        var playerResource = GameObject.Find("/PlayerResource/Spellbar");
        if (sprite != null)
        {
            playerResource.transform.GetChild(spellSlot).Find("BackgroundSprite").GetComponent<Image>().sprite = sprite;
        }
    }

    protected virtual IEnumerator DoSpell()
    {
        throw new NotImplementedException();
    }

    protected virtual IEnumerator AnimatePlayer()
    {
        throw new NotImplementedException();
    }

    protected void StopCurrentAnimation(string name)
    {
        _playerAnimator.StopAnimatingSpell(name);
    }
}
