using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicMeshCutter;

public class SliceObject : CutterBehaviour
{
    public Transform planeDebug;

    [SerializeField] AudioClip[] Slice;
    [SerializeField] AudioSource sliceSound;

    private void Cut(GameObject target)
    {
        Plane plane = new Plane(planeDebug.up, planeDebug.position);
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
            sliceSound.clip = Slice[Random.Range(0, 2)];
            sliceSound.Play();
            collision.transform.tag = "Untagged";
            Cut(collision.gameObject);
            collision.gameObject.GetComponent<AI>().Death();
        }
    }
}
