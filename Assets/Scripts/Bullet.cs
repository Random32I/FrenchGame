using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] AudioSource eat;

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
            Destroy(gameObject);
        }
    }
}
