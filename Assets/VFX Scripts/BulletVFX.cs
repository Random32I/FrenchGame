using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BulletVFX : MonoBehaviour
{
    [SerializeField] AudioSource eat;
    [SerializeField] VisualEffect hitVFX;
    [SerializeField] VisualEffect bloodVFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.tag != "Shot")
        {
            if (collision.transform.name == "HeadCollider")
            {
                eat = collision.transform.GetComponent<AudioSource>();
                eat.Play();
                Destroy(gameObject);
            }
        }
        else if (transform.tag == "Shot")
        {
            Instantiate(hitVFX, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            Instantiate(bloodVFX, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            hitVFX.Play();
            bloodVFX.Play();
            Destroy(gameObject);
        }
    }
}
