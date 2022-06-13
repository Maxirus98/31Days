using System;
using UnityEngine;

public class Action : MonoBehaviour
{
    public float Cooldown { get; set; }
    public float Timestamp { get; set; }
    public string Name { get; set; }
    
    private void Start()
    {
        instantiateAction();
    }

    protected virtual void instantiateAction()
    {
        Cooldown = 0f;
        Name = "";
    }
}
