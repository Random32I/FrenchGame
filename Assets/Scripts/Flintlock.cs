using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Flintlock : MonoBehaviour
{
    bool loaded;
    int gunpowderAmount = 1; //set to 0 when actually implementing gunpowder
    [SerializeField] float unmodifiedForce = 20;
    [SerializeField] GameObject bullet = null;
    [SerializeField] Transform Angle;
    bool fired;

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
                bullet.SetActive(true);
                bullet.transform.position = transform.position + new Vector3(-0.00940001849f, 0.00177140732f, 0.0196000002f);
                bullet.transform.tag = "Shot";
                bullet.GetComponent<Rigidbody>().AddForce(transform.parent.forward * unmodifiedForce * gunpowderAmount, ForceMode.VelocityChange);
                loaded = false;
                bullet = null;
            }
        }
    }

    

    public void LoadGun(GameObject newBullet)
    {
        bullet = newBullet;
        loaded = true;
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
