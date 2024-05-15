using UnityEngine;

public class HoldController : MonoBehaviour
{
    [SerializeField] Transform holdPos;
    [SerializeField] float pickupRange;
    [SerializeField] float pickupSpeed;
    [SerializeField] float launchStrength;


    [HideInInspector] public GameObject heldObj;
    [SerializeField] Rigidbody rb;

    [HideInInspector] public bool hasCube = false;

    private int layerToShow;

    private void Start()
    {
        layerToShow = LayerMask.NameToLayer("HeldObj");
    }
    private void Update()
    {
        if (Input.GetButtonDown("Pickup"))
        {
            if (heldObj == null)
            {
                pickUp();
            }
            else 
            {
                winCube cube = heldObj.GetComponent<winCube>();
                doNotDropThroughObj();
                drop();
                cube.ApplyVelocity(GameManager.Instance.playerCntrl.GetVelocity());
            }
        }
        if (heldObj != null)
        {
            if (Input.GetMouseButtonDown(0) && GameManager.Instance.activeMenu == null)
            {
                winCube cube = heldObj.GetComponent<winCube>();
                doNotDropThroughObj();
                drop();
                cube.ApplyVelocity((Camera.main.transform.forward * launchStrength) + GameManager.Instance.playerCntrl.GetVelocity());
            }
            //if the pickup object is not close enough to the player move the object to the hold location
            else if(Vector3.Distance(heldObj.transform.position, holdPos.position) > 0.1f)
            {
                Vector3 moveDirection = holdPos.position - heldObj.transform.position;
                rb.AddForce(moveDirection * pickupSpeed);
            }
        }
        if (hasCube && Vector3.Distance(rb.position, holdPos.position) < 0.1f)
        {
            rb.transform.position = holdPos.position;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            //rb.isKinematic = true;
            heldObj.GetComponent<Collider>().isTrigger = true;
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
                heldObj = hit.transform.gameObject;
                heldObj.layer = layerToShow;
                //heldObj.GetComponent<Collider>().enabled = false;
                rb = hit.transform.gameObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.drag = 8;

                rb.transform.parent = holdPos;

                GameManager.Instance.playerCntrl.runSpeed -= GameManager.Instance.playerCntrl.holdSpeed;
            }
        }
    }

    public void drop()
    {
        hasCube = false;
        if (heldObj != null)
        {
            //drop
            heldObj.layer = 0;
            //heldObj.GetComponent<Collider>().enabled = true;
            heldObj.GetComponent<Collider>().isTrigger = false;
            rb.useGravity = true;
            rb.drag = .3f;
            rb.constraints = RigidbodyConstraints.None;
            //rb.isKinematic = false;
            heldObj.transform.parent = null;
            heldObj = null;

            GameManager.Instance.playerCntrl.runSpeed += GameManager.Instance.playerCntrl.holdSpeed;
        }
    }
    void doNotDropThroughObj()
    {
        float rangeToCube = Vector3.Distance(heldObj.transform.position, transform.position) + 1;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), rangeToCube);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red, rangeToCube);
        if (hits.Length > 0) 
        {
            heldObj.transform.position = transform.position + new Vector3(0f, 1f, 0f);
        }
    }
}
