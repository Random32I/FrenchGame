using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using 

public class Bullet : MonoBehaviour
{
    [SerializeField] AudioSource eat;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "HeadCollider")
        {
            eat.Play();
            Destroy(gameObject);
        }
    }
}
