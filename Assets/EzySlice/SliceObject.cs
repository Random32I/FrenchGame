using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Transform planeDebug;
    public GameObject target;
    public Material crossSectionMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Slice(GameObject target)
    {
        SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);

            upperHull.AddComponent<Rigidbody>();
            upperHull.AddComponent<MeshCollider>();
            upperHull.GetComponent<MeshCollider>().convex = true;

            lowerHull.AddComponent<Rigidbody>();
            lowerHull.AddComponent<MeshCollider>();
            lowerHull.GetComponent<MeshCollider>().convex = true;

            Destroy(target);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Sliceable")
        {
            Slice(collision.gameObject);
        }
    }
}
