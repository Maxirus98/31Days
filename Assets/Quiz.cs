using System;
using UnityEngine;
using UnityEngine.AI;

public class Quiz : MonoBehaviour
{
    [SerializeField]private GameObject interactObject;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            QuizMethod();
        }
    }

    private void QuizMethod()
    {
        var direction = transform.position - interactObject.transform.position;
        direction.Normalize();
        interactObject.transform.rotation = 
            Quaternion.Slerp(interactObject.transform.rotation,
            Quaternion.LookRotation(direction),
            5f * Time.deltaTime);
    }
}
