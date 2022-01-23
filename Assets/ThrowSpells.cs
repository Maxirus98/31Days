using UnityEngine;

public class ThrowSpells : Spells
{
    [SerializeField] protected float TravelSpeed { get; set; }
    [SerializeField] protected GameObject _initialSummon;
    protected float TravelTime { get; set; }
}
