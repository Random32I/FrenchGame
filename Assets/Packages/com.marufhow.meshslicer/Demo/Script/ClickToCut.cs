using com.marufhow.meshslicer.core;
using UnityEngine;

namespace com.marufhow.meshslicer.demo
{
    public class ClickToCut : MonoBehaviour
    {
        [Header("Click to cut target vertically. Press SHIFT to cut horizontally")] [SerializeField]
        private MHCutter _mhCutter;

        private void Update()
        {
            /*if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("Ground")) return;

                    // Check if the Shift key is held down
                    Vector3 cutDirection = Camera.main.transform.forward;

                    _mhCutter.Cut(hit.collider.gameObject, hit.point, cutDirection);
                }
            }*/
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Sliceable")
            {
                Vector3 cutDirection = gameObject.GetComponent<Rigidbody>().velocity;
                _mhCutter.Cut(collision.gameObject, transform.position, cutDirection);
            }
        }

    }
}


 