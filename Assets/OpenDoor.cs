using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private LayerMask _player;
    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 5;
        Debug.DrawRay(transform.position + Vector3.up, forward, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, forward, out hit, 5f, LayerMask.GetMask("Ignore Raycast")))
        {
            print("hit " + hit.transform.name);
            var jointMotor = gameObject.GetComponent<HingeJoint>();
            jointMotor.useMotor = true;
        }
    }
}
