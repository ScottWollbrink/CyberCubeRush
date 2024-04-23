using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerController : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController controller;

    [Header("Player Basics")]
    [SerializeField] int maxHP;
    private int currentHP;
    [SerializeField] int speed;
    [SerializeField] int sprintSpeed;
    [SerializeField] int jumpSpeed;
    [SerializeField] int maxJumps;
    [SerializeField] int gravity;

    [Header("WallJump")]
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask groundMask;
    [SerializeField] int maxWallJumps;
    [SerializeField] float distanceToWallCheck;
    [SerializeField] float distanceToGround;
    [SerializeField] int wallJumpSpeed;

    [Header("Shooting")]
    [SerializeField] int shootDamage;
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;

    Vector3 moveDir;
    Vector3 playerVel;
    bool isShooting;
    int jumpedTimes;
    int wallJumpTimes;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    bool wallLeft;
    bool wallRight;
    private Vector3 platformMovement;
    private Transform platform;
    private Vector3 lastPlatfromPosition;
    bool canLeavePlatform;


    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        UpdatePlayerUI();
    }

    // Update is called once per frame
    void Update()
    {
        //draw debug ray to see how far player can shoot
        Debug.DrawRay(Camera.main.transform.position + (Camera.main.transform.forward * .5f), Camera.main.transform.forward * shootDist, Color.blue);

        movement();
        WallCheck();
    }

    void movement()
    {
        // reset jump if player is on the ground
        if (controller.isGrounded)
        {
            jumpedTimes = 0;
            wallJumpTimes = 0;
            playerVel = Vector3.zero;
        }

        if (platform != null)
        {
            platformMovement = platform.position - lastPlatfromPosition;
            float speedScale = 0.825f; // not implemented correctly, causing the platform boosting while jumping
            playerVel += platformMovement * Time.deltaTime;//(speedScale / Time.deltaTime);

            lastPlatfromPosition = platform.position;
            canLeavePlatform = true;
        }
        else
        {
            if (canLeavePlatform)
            {
                float speedScale = 0.825f;
                playerVel -= platformMovement * (speedScale / Time.deltaTime);
                lastPlatfromPosition = transform.position;
                canLeavePlatform = false;
            }
        }

        // get movemetn input and multiply by there movement vectors
        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        // move the controler in the direction inputed
        controller.Move(moveDir * speed * Time.deltaTime);
        // check to see if player is pressing the shoot button and can shoot
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += sprintSpeed;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            speed -= sprintSpeed;
        }

        if (Input.GetButton("Shoot") && !isShooting)
        {
            StartCoroutine(shoot());
        }

        // check to see if player is pressing the jump button and is not over the max number of concurent jumps
        
        if (Input.GetButtonDown("Jump") && wallJumpTimes < maxWallJumps && offTheGround() && (wallRight || wallLeft))
        {
            WallJump();
        }
        else if (Input.GetButtonDown("Jump") && jumpedTimes < maxJumps) 
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
            if (hit.transform != transform && dmg != null)
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
        currentHP -= amount;
        UpdatePlayerUI();
        StartCoroutine(FlashDamage());

        if (currentHP <= 0) 
        {
            GameManager.Instance.LoseGame();
        }
    }

    IEnumerator FlashDamage()
    {
        GameManager.Instance.playerDamageScreen.SetActive(true);
        yield return new WaitForSeconds(.1f);
        GameManager.Instance.playerDamageScreen.SetActive(false);
    }

    private void UpdatePlayerUI()
    {
        GameManager.Instance.playerHPBar.fillAmount = (float)currentHP / maxHP;
    }

    private void WallJump()
    {
        Vector3 wallNormal;
        Vector3 wallJumpforce;
        if (wallRight)
        {
            wallNormal = rightWallHit.normal;
            wallJumpforce = transform.up * jumpSpeed + wallNormal * wallJumpSpeed;
            if (jumpedTimes > 0)
            {
                jumpedTimes--;
            }
            wallJumpTimes++;
            playerVel = wallJumpforce;
        }
        else if (wallLeft)
        {
            wallNormal = leftWallHit.normal;
            wallJumpforce = transform.up * jumpSpeed + wallNormal * wallJumpSpeed;
            if (jumpedTimes > 0) 
            { 
                jumpedTimes--; 
            }
            wallJumpTimes++;
            playerVel = wallJumpforce;
        }
        
    }

    private void WallCheck()
    {
        wallRight = Physics.Raycast(transform.position, transform.right, out rightWallHit, distanceToWallCheck, wallMask);
        wallLeft = Physics.Raycast(transform.position, -transform.right, out leftWallHit, distanceToWallCheck, wallMask);
    }
    private bool offTheGround()
    {
        return !Physics.Raycast(transform.position, -transform.up, distanceToGround, groundMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            platform = other.transform;
            transform.parent = platform;

            lastPlatfromPosition = platform.position;
            canLeavePlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            transform.parent = null;
            platform = null;
            canLeavePlatform = false;
        }
    }
}
