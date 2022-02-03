public class PlayerCombat : CharacterCombat
{
    private PlayerStats _playerStats;
    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _onDeath = Die;
    }

    private void Update()
    {
        if (_healthbarScript == null && _playerStats.isActiveAndEnabled)
        {
            _healthbarScript = _playerStats.hp.GetComponent<HealthbarScript>();
        }
    }

    private new void Die()
    {
        isDead = true;
        _animator.SetTrigger("Die");
        Invoke(nameof(UpdateGameStateAfterDeath), 2f);
    }

    private void UpdateGameStateAfterDeath()
    {
        UpdateCharacterState(CharacterState.Dead);
    }
}
