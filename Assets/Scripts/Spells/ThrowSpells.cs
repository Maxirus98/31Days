using UnityEngine;

public class ThrowSpells : Spells
{
    [SerializeField] protected float TravelSpeed { get; set; }
    [SerializeField] protected GameObject _initialSummon;
    public float TravelTime { get; set; }
}
