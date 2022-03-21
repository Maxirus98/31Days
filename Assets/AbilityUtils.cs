using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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

    public static Vector3  DirectAt(Transform transform, Interactable focus)
    {
        Vector3 fromPosition = transform.position;
        Vector3 toPosition = focus.transform.position;
        Vector3 direction = toPosition - fromPosition;

        return direction;
    }
}
