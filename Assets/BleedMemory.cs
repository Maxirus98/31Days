/// <summary>
/// Warrior first memory (Index 0)
/// </summary>
public class BleedMemory : Memory
{
    /// <summary>
    /// Name of the GameObject found on the Enemy
    /// </summary>
    private readonly string BLEED_DEBUFF = "BleedDebuff";

    protected override void Start()
    {
        base.Start();
        Title = "Bleed";
        Description = "Make the enemy bleed";
        Type = MemoryType.EnemyDebuff;
        Index = 0;
    }
}
