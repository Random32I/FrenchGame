using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Movement : MonoBehaviour
{
    [SerializeField] SteamVR_Action_Boolean turnLeft;
    [SerializeField] SteamVR_Action_Boolean turnRight;
    [SerializeField] SteamVR_Action_Boolean move;
    [SerializeField] GameObject RightHand;
    [SerializeField] GameObject LeftHand;
    [SerializeField] Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnLeft.state)
        {
            transform.Rotate(Vector3.down);
        }
        else if (turnRight.state)
        {
            transform.Rotate(Vector3.up);
        }
        if (move.state)
        {
            Vector3 direction = Vector3.zero;
            if (move.activeDevice.ToString() == "RightHand")
            {
                direction = RightHand.transform.rotation.eulerAngles.normalized;
            }
            else if (move.activeDevice.ToString() == "LeftHand")
            {
                direction = LeftHand.transform.rotation.eulerAngles.normalized;
            }

            rig.AddForce(new Vector3(direction.x, 0, direction.z), ForceMode.Force);
        }
    }
}
