using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ItemRemoval : MonoBehaviour
{
    float timeStamp;
    bool held;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!held)
        {
            if (Time.timeSinceLevelLoad - timeStamp >= 20)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Grab()
    {
        held = true;
    }

    public void Release()
    {
        held = false;
        timeStamp = Time.timeSinceLevelLoad;
    }
}
