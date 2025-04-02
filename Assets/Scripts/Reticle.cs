using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Reticle : MonoBehaviour
{
    public float defaultLength = 5f;
    public GameObject reticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit = CreateRaycast(defaultLength);

        Vector3 endPosition = transform.position + (-transform.right * defaultLength);

        if (hit.collider != null)
        {
            if (hit.distance < 1)
            {
                reticle.SetActive(false);
            }
            else
            {
                endPosition = hit.point + hit.normal * 0.01f;
                reticle.transform.rotation = Quaternion.LookRotation(hit.normal);
                reticle.transform.Rotate(new Vector3(90, 0, 0));
                reticle.SetActive(true);
            }
        }
        else
        {
            reticle.SetActive(false);
        }

        reticle.transform.position = endPosition;
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.right);
        Physics.Raycast(ray, out hit, defaultLength);

        return hit;
    }
}
