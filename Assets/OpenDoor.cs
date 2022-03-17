using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 5;
        Debug.DrawRay(transform.position + Vector3.up, forward, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, forward, out hit))
        {
            print("hit " + hit.transform.name);
            if (hit.transform.CompareTag("Player"))
            {
                var jointMotor = gameObject.GetComponent<HingeJoint>();
                jointMotor.useMotor = true;
            }
        }
    }
}
