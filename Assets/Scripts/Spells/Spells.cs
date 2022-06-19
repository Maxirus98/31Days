using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spells : MonoBehaviour
{
    /// <summary>
    /// SpellBar Ui Slot Index
    /// </summary>
    public int spellSlot;
    public delegate void AttackCallback(CharacterCombatState characterCombatState);
    public AttackCallback attackCallback;
    public string Name { get; set; }
    public string Description { get; set; }
    public float cooldown;
    public float Timestamp { get; set; }

    public float BaseDamage { get; set; }
    public bool IsAutoTarget { get; set; }
    public float Range { get; set; }
    public float hitRadius = 1f;
    
    [SerializeField] protected LayerMask enemyLayers;
    [SerializeField] protected PlayerAnimator playerAnimator;
    [SerializeField] protected Sprite sprite;
    
    protected Animator animator;
    protected CharacterCombat CharacterCombat;
    protected GameObject spellBar;
    
    
    
    protected virtual void Start()
    {
        CharacterCombat = GetComponent<CharacterCombat>();
        attackCallback = CharacterCombat.UpdateCharacterCombatState;
        playerAnimator = GetComponent<PlayerAnimator>();
        animator = GetComponent<Animator>();
        InitSpellUi();
    }

    // TODO: Refactor to find a better way than waiting on Start
    private void InitSpellUi()
    {
        if (sprite == null) return;

        spellBar = GameObject.Find("/HUD/Spellbar");
        spellBar.transform.GetChild(spellSlot).Find("BackgroundSprite").GetComponent<Image>().sprite = sprite;
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
        playerAnimator.StopAnimatingSpell(name);
    }
}
