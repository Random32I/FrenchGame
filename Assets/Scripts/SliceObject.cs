using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicMeshCutter;

public class SliceObject : CutterBehaviour
{
    public Transform planeDebug;
    // public GameObject target;
    //public Material crossSectionMaterial;

    private void Cut(GameObject target)
    {
        Plane plane = new Plane(planeDebug.position, planeDebug.up);
        var targets = target.GetComponentsInChildren<MeshTarget>();
        foreach (var i in targets)
        {
            Cut(i.GetComponent<MeshTarget>(), transform.position, plane.normal, null, OnCreated);
        }
    }
    void OnCreated(Info info, MeshCreationData cData)
    {
        MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
    }

    /*public void Slice(GameObject target)
    {
        SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);

            upperHull.AddComponent<Rigidbody>();
            upperHull.AddComponent<MeshCollider>();
            upperHull.GetComponent<MeshCollider>().convex = true;
            upperHull.AddComponent<SliceDisapear>();

            lowerHull.AddComponent<Rigidbody>();
            lowerHull.AddComponent<MeshCollider>();
            lowerHull.GetComponent<MeshCollider>().convex = true;
            lowerHull.AddComponent<SliceDisapear>();

            target.GetComponent<AI>().Death();
            target.transform.position = Vector3.zero;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Sliceable")
        {
            Cut(collision.gameObject);
        }
    }
}
