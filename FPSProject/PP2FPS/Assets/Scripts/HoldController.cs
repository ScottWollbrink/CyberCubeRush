using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldController : MonoBehaviour
{
    [SerializeField] Transform holdPos;
    [SerializeField] float pickupRange;
    [SerializeField] float pickupSpeed;


    [HideInInspector] public GameObject hold;
    [SerializeField] Rigidbody rb;

    [HideInInspector] public bool hasCube;

    private void Update()
    {
        if (Input.GetButtonDown("Pickup"))
        {
            if (hold == null)
            {
                pickUp();
            }
            else
            {
                drop();
            }
        }
        if (hold != null)
        {   
            //if the pickup object is not close enough to the player move the object to the hold location
            if (Vector3.Distance(hold.transform.position, holdPos.position) > 0.1f)
            {
                Vector3 moveDirection = holdPos.position - hold.transform.position;
                rb.AddForce(moveDirection * pickupSpeed );
            }
        }
    }

    public void pickUp()
    {
        RaycastHit hit;
        // added transform.forward * .5f to fix a bug where the cube could not be picked up close to the player
        if (Physics.Raycast(transform.position + (transform.forward * .5f), transform.TransformDirection(Vector3.forward), out hit, pickupRange))
        {
            //Pickup
            if (hit.transform.gameObject.GetComponent<Rigidbody>())
            {
                hasCube = true;
                rb = hit.transform.gameObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.drag = 8;
                rb.constraints = RigidbodyConstraints.FreezeRotation;

                rb.transform.parent = holdPos;
                hold = hit.transform.gameObject;

                GameManager.Instance.playerCntrl.runSpeed -= GameManager.Instance.playerCntrl.holdSpeed;
            }
        }
    }

    public void drop()
    {
        hasCube = false;
        if (hold != null)
        {
            //drop
            rb.useGravity = true;
            rb.drag = 1;
            rb.constraints = RigidbodyConstraints.None;

            hold.transform.parent = null;
            hold = null;

            GameManager.Instance.playerCntrl.runSpeed += GameManager.Instance.playerCntrl.holdSpeed;
        }
    }
}
