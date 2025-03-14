using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Valve.VR;

public class SwordVFX : MonoBehaviour
{
    [ExecuteAlways]

    [SerializeField] VisualEffect vfx; // Assigns VFX component
    [SerializeField] float angularVelocityThreshold = 6.5f; // angular detection to activate sword VFX

    private float logTime = 0f; // Timer to avoid Debug clutter
    private float interval = 4f; // Log every 4 seconds

    private Rigidbody body;

    Vector3 prevPos = Vector3.zero;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        if (vfx == null)
        {
            Debug.LogError("VFX not assigned");
        }
    }

    void Update()
    {

        float angularVelocity;

        if (transform.parent)
        {
            angularVelocity = Mathf.Abs((transform.parent.position - prevPos).magnitude);
            prevPos = transform.parent.position;
        }
        else
        {
            angularVelocity = 0;
        }

        if (body != null && vfx != null)
        {
            bool vfxActive = angularVelocity > angularVelocityThreshold;
            vfx.enabled = vfxActive;
        }

        // Only log every debugInterval seconds
        if (Time.time >= logTime)
        {
            logTime = Time.time + interval;
        }
    }
}
