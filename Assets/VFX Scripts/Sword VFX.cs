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
        float angularVelocity = body.angularVelocity.magnitude;

        if (body != null && vfx != null)
        {
            bool vfxActive = angularVelocity > angularVelocityThreshold;
            vfx.enabled = vfxActive;
        }

        // Only log every debugInterval seconds
        if (Time.time >= logTime)
        {
            Debug.Log($"Angular Velocity: {angularVelocity}");
            logTime = Time.time + interval;
        }
    }
}
