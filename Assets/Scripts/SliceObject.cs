using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicMeshCutter;

public class SliceObject : CutterBehaviour
{
    public Transform planeDebug;

    private void Cut(GameObject target)
    {
        Plane plane = new Plane(planeDebug.position, planeDebug.up);
        Cut(target.GetComponent<MeshTarget>(), transform.position, plane.normal, null, OnCreated);
    }
    void OnCreated(Info info, MeshCreationData cData)
    {
        MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Sliceable")
        {
            collision.gameObject.GetComponent<AI>().Death();
            Cut(collision.gameObject);
        }
    }
}
