using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private CharacterStats _characterStats;

    private void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
    }

    private void TakeDamage(float damage)
    {
        float damageDone = damage / (_characterStats.Defense * _characterStats.CurrentDefenseMultiplier);
        _characterStats.Health -= damageDone;
        Debug.Log(gameObject.name + "hit for " + damageDone + " damage");
        Die();
    }

    public void Die()
    {
        if(_characterStats.Health <= 0)
            Debug.Log(gameObject.name + " just died");
    }
}
