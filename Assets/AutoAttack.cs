using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : Action
{
    public float[] Attacks { get; set; }
    
    //TODO: Rotate between 2-3 attack animations
    public int currentAttackIndex;
    public float range;
    protected override void instantiateAction()
    {
        Cooldown = 1f;
        Name = "Auto Attack";
        range = 15f;
        Attacks = new []{1f, 2f, 3f};
        currentAttackIndex = 0;
    }
}
