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

    bool isShot;

    string heldHandName;
    bool isHeld;

    [SerializeField] SteamVR_Action_Boolean shoot1;
    [SerializeField] SteamVR_Action_Boolean shoot2;
    SteamVR_Action_Boolean shootAction;

    // Start is called before the first frame update
    void Start()
    {
        shootAction = shoot1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent)
        {
            if (shootAction.state && shootAction.activeDevice.ToString().Equals(transform.parent.name) && loaded && !isShot)
            {
                bulletCount--;
                bullets[bulletCount].SetActive(true);
                bullets[bulletCount].transform.position = transform.position + new Vector3(-0.10940001849f, 0.14177140732f, 0.0196000002f);
                bullets[bulletCount].transform.tag = "Shot";
                bullets[bulletCount].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                bullets[bulletCount].GetComponent<Rigidbody>().AddForce((transform.right ) * -unmodifiedForce, ForceMode.VelocityChange);
                bullets[bulletCount] = null;
                fullyLoaded = false;
                loaded = bullets[0] != null;
                Fire.Play();
                isShot = true;
            }
            if (!shootAction.state)
            {
                isShot = false;
            }
        }
    }

    

    public void LoadGun(GameObject newBullet)
    {
        if (bullets[4] == null)
        {
            bullets[bulletCount] = newBullet;
            loaded = true;
            fullyLoaded = bullets[4] != null;
            bulletCount++;
            Reload.Play();
        }
    }

    public void PickedUp()
    {
        isHeld = true;
        heldHandName = transform.parent.name;
        if (shoot1.state)
        {
            shootAction = shoot2;
        }
        else if (shoot2.state)
        {
            shootAction = shoot1;
        }
        else
        {
            shootAction = shoot1;
        }
    }

    public void Dropped()
    {
        isHeld = false;
        heldHandName = null;
    }
}
