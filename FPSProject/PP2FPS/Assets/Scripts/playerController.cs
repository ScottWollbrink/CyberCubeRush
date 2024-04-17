using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController controller;

    [SerializeField] int HP;
    [SerializeField] int speed;
    [SerializeField] int jumpSpeed;
    [SerializeField] int maxJumps;
    [SerializeField] int gravity;

    [SerializeField] int shootDamage;
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;

    Vector3 moveDir;
    Vector3 playerVel;
    bool isShooting;
    int jumpedTimes;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //draw debug ray to see how far player can shoot
        Debug.DrawRay(Camera.main.transform.position + (Camera.main.transform.forward * .5f), Camera.main.transform.forward * shootDist, Color.blue);

        movement();

    }

    void movement()
    {
        // reset jump if player is on the ground
        if (controller.isGrounded)
        {
            jumpedTimes = 0;
            playerVel = Vector3.zero;
        }
        // get movemetn input and multiply by there movement vectors
        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        // move the controler in the direction inputed
        controller.Move(moveDir * speed * Time.deltaTime);
        // check to see if player is pressing the shoot button and can shoot
        if (Input.GetButton("Shoot") && !isShooting)
        {
            StartCoroutine(shoot());
        }

        // check to see if player is pressing the jump button and is not over the max number of concurent jumps
        if (Input.GetButtonDown("Jump") && jumpedTimes < maxJumps)
        {
            jumpedTimes++;
            playerVel.y = jumpSpeed;
        }
        // add gravity to the player so that they fall when going over and edge or jump
        playerVel.y -= gravity * Time.deltaTime;
        controller.Move(playerVel * Time.deltaTime);

    }

    IEnumerator shoot()
    {
        isShooting = true;
        // create a Raycasthit to pass into physics raycast
        RaycastHit hit;
        // create a raycast and check to see if it hit something
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
        {
            // create a IDamage called dmg to hold information of the object hit
            IDamage dmg = hit.collider.GetComponent<IDamage>();
            // checkt to see if dmg has an IDmage
            if (hit.transform != transform &&dmg != null)
            {
                // pass damage to dmg take damage method
                dmg.takeDamage(shootDamage);
            }
        }
        // create a timer that will last for the time passed in by shootRate
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    public void takeDamage(int amount)
    {
        HP -= amount;

        if (HP <= 0) 
        {
            GameManager.Instance.LoseGame();
        }
    }
}
