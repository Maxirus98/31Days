public class PlayerCombat : CharacterCombat
{
    private PlayerStats _playerStats;
    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        OnDeathCallback = Die;
    }

    private void Update()
    {
        if (healthbarScript == null && _playerStats.isActiveAndEnabled)
        {
            healthbarScript = _playerStats.hp.GetComponent<HealthbarScript>();
        }
    }
}
