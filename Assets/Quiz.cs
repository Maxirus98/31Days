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
            AMethod();
        }

        if ((interactObject.transform.position - transform.position).sqrMagnitude <= 1.5f)
        {
            interactObject.GetComponent<Animator>().SetFloat("VelocityZ", 0f);
        }
    }

    private void AMethod()
    {
        interactObject.GetComponent<NavMeshAgent>().destination = transform.position;
        interactObject.GetComponent<Animator>().SetFloat("VelocityZ", 1f);
    }
}
