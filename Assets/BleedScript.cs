using UnityEngine;

public class BleedScript : Memory
{
    [SerializeField]private BoomerangSwordScript boomerangSwordScript;
    protected override void Start()
    {
        base.Start();
    }

    protected override void RememberMemory()
    {
        base.RememberMemory();
        // Get BoomerangSwordScript
        boomerangSwordScript.Bleed = () => Invoke(nameof(Bleed), 0f);
        // Add effect to BoomerangSwordScript
    }

    private void Bleed()
    {
        
    }
}
