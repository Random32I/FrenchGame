using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Flintlock : MonoBehaviour
{
    bool loaded;
    int gunpowderAmount = 1; //set to 0 when actually implementing gunpowder
    [SerializeField] float unmodifiedForce = 20;
    [SerializeField] GameObject[] bullets = new GameObject[5];
    [SerializeField] Transform Angle;
    [SerializeField] AudioSource Fire;
    [SerializeField] AudioSource Reload;
    bool fired;
    public bool fullyLoaded;
    int bulletCount;

    string heldHandName;
    bool isHeld;

    [SerializeField] SteamVR_Action_Boolean shoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent)
        {
            if (shoot.state && shoot.activeDevice.ToString().Equals(transform.parent.name) && loaded)
            {
                bulletCount--;
                bullets[bulletCount].SetActive(true);
                bullets[bulletCount].transform.position = transform.position + new Vector3(-0.00940001849f, 0.00177140732f, 0.0196000002f);
                bullets[bulletCount].transform.tag = "Shot";
                bullets[bulletCount].GetComponent<Rigidbody>().AddForce(transform.parent.forward * unmodifiedForce * gunpowderAmount, ForceMode.VelocityChange);
                bullets[bulletCount] = null;
                fullyLoaded = false;
                loaded = bullets[0] == null;
                Fire.Play();
            }
        }
    }

    

    public void LoadGun(GameObject newBullet)
    {
        if (bullets[4] == null)
        {
            bullets[bulletCount] = newBullet;
            loaded = true;
            fullyLoaded = bullets[4] == null;
            bulletCount++;
            Reload.Play();
        }
    }

    public void PickedUp()
    {
        isHeld = true;
        heldHandName = transform.parent.name;
    }

    public void Dropped()
    {
        isHeld = false;
        heldHandName = null;
    }
}
