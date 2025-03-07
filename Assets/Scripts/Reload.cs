using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    [SerializeField] Flintlock gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet" && other.transform.tag != "Shot")
        { 

            if (!gun.fullyLoaded)
            {
                other.gameObject.SetActive(false);
                gun.LoadGun(other.gameObject);
            }
        }
    }
}
