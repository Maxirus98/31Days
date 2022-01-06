using UnityEngine;

public class SkillShotSpell : Spells
{
    public Vector3 AreaScale { get; set; }
    public float MaxRange { get; set; }
    public GameObject spawnArea;

    public void ToggleAbilityRange(bool isShown, GameObject abilityRange)
    {
        abilityRange.gameObject.SetActive(isShown);
    
    }

    public void SetAreaScale(Transform abilityRange)
    {
        abilityRange.localScale = AreaScale;
    }
}
