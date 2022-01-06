using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUtils
{
    public static Transform FindDeepChild(string name, Transform parent)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(parent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == name)
                return c;
            foreach(Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }
}
