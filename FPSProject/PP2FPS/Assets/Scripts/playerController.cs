using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController controller;
    [SerializeField] AudioSource aud;

    [Header("Player Basics")]
    [SerializeField] float maxHP;
    private float currentHP;
    [SerializeField] public int runSpeed;
    [SerializeField] public int holdSpeed;
    [SerializeField] int jumpSpeed;
    [SerializeField] int maxJumps;
    [SerializeField] float gravity;

    [Header("Wall Jump")]
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask movingPlatformMask;
    [SerializeField] int maxWallJumps;
    [SerializeField] float distanceToWallCheck;
    [SerializeField] float distanceToGround;
    [SerializeField] int wallJumpSpeed;
    [SerializeField] int wallJumpVertSpeed;
    [SerializeField] float timeToTurnOffHorizontalMovement;

    [Header("Wall Run")]
    [SerializeField] float wallRunGravity;
    [SerializeField] int wallRunSpeed;
    [SerializeField] float wallRunDuration;

    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDurationWhileGrounded;
    [SerializeField] float dashCooldown;


    [Header("Shooting")]
    [SerializeField] int shootDamage;
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] List<GunStats> gunList = new List<GunStats>();
    [SerializeField] GameObject gunModel;

    [Header("Audio")]
    [SerializeField] AudioClip[] audJump;
    [SerializeField, Range(0, 1f)] float audJumpVol;
    [SerializeField] AudioClip[] audHurt;
    [SerializeField, Range(0, 1f)] float audHurtVol;
    [SerializeField] AudioClip[] audSteps;
    [SerializeField, Range(0, 1f)] float audStepVol;
    [SerializeField] AudioClip[] audShoot;
    [SerializeField, Range(0, 1f)] float audShootVol;
    [SerializeField] AudioClip audDeath;
    [SerializeField, Range(0, 1f)] float audDeathVol;

    
    int selectedGun;

    Vector3 moveDir;
    Vector3 playerVel;
    float gravityOriginal;
    int speedOriginal;
    bool isShooting;
    bool isSprinting;
    bool isPlayingSteps;
    int jumpedTimes;
    int wallJumpTimes;
    bool HorizontalInputEnabled;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    bool wallLeft;
    bool wallRight;
    private GameObject platform;
    private float platformSpeed;
    float horizontalInput;
    bool canDash = true;
    bool isDashing;
    bool isWallRunning = false;



    // Start is called before the first frame update
    void Start()
    {
            HorizontalInputEnabled = true;
            currentHP = maxHP;
            gravityOriginal = gravity;
            speedOriginal = runSpeed;
            SpawnPlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
        //draw debug ray to see how far player can shoot
        Debug.DrawRay(Camera.main.transform.position + (Camera.main.transform.forward * .5f), Camera.main.transform.forward * shootDist, Color.blue);

        if (!GameManager.Instance.isPaused)
        {
            SelectGun();
            movement();
            WallCheck();
        }
        //sprint();
    }

    void movement()
    {
        // reset jump if player is on the ground
        if (controller.isGrounded)
        {
            jumpedTimes = 0;
            wallJumpTimes = 0;
            if (!isDashing)
            {
                playerVel = Vector3.zero;
            }
            HorizontalInputEnabled = true;
        }
        

        // get movemetn input and multiply by there movement vectors
        if (CheckForPlatform())
        {
            horizontalInput = HorizontalInputEnabled ? Input.GetAxis("Horizontal") : 0f;
            float verticalInput = Input.GetAxis("Vertical");
            if (horizontalInput != 0 && playerVel.z != 0)
            {
                playerVel.z -= .25f;
            }
            moveDir = horizontalInput * transform.right + verticalInput * transform.forward;
            if (moveDir == Vector3.zero && !Input.GetButton("Jump"))
            {
                controller.enabled = false;
            }
            else
            {
                controller.enabled = true;
                controller.Move(moveDir * runSpeed * Time.deltaTime);
            }
        }
        else
        {
            // move the controler in the direction inputed
            horizontalInput = HorizontalInputEnabled ? Input.GetAxis("Horizontal") : 0f;
            float verticalInput = Input.GetAxis("Vertical");
            if (horizontalInput != 0 && playerVel.z != 0)
            {
                playerVel.z -= .01f;
            }
            moveDir = horizontalInput * transform.right + verticalInput * transform.forward;
            controller.Move(moveDir * runSpeed * Time.deltaTime);
        }

        if (controller.isGrounded && moveDir.normalized.magnitude > 0.3f && !isPlayingSteps)
        {
            StartCoroutine(PlaySteps());
        }

        if (Input.GetButton("Shoot") && !isShooting && gunList.Count > 0 && gunList[selectedGun].ammoCurrent > 0)
        {
            StartCoroutine(shoot());
        }

        if (Input.GetButtonDown("Dash") && canDash)
        {
            Dash();
        }

        if((wallLeft || wallRight) && offTheGround() && !isWallRunning)
        {
            WallRunStart();
        }
        else if (((!wallLeft && !wallRight) || !offTheGround()) && isWallRunning) 
        {
            WallRunStop();
        }

        // check to see if player is pressing the jump button and is not over the max number of concurent jumps
        
        if (Input.GetButtonDown("Jump") && wallJumpTimes < maxWallJumps && offTheGround() && (wallRight || wallLeft))
        {
            WallJump();
            aud.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
        }
        else if (Input.GetButtonDown("Jump") && jumpedTimes < maxJumps) 
        {
            controller.enabled = true;
            jumpedTimes++;
            playerVel.y = jumpSpeed;
            aud.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
        }

        // add gravity to the player so that they fall when going over and edge or jump
        if (controller.enabled)
        {
            playerVel.y -= gravity * Time.deltaTime;
            controller.Move(playerVel * Time.deltaTime);
        }

        
    }

    IEnumerator PlaySteps()
    {
        isPlayingSteps = true;

        aud.PlayOneShot(audSteps[Random.Range(0, audSteps.Length)], audStepVol);

        // this means interval equals 0.3f if sprinting, else interval = 0.5f
        float interval = (isSprinting) ? 0.3f : 0.5f;
        yield return new WaitForSeconds(interval);
        isPlayingSteps = false;
    }

    IEnumerator shoot()
    {
        isShooting = true;
        aud.PlayOneShot(audShoot[Random.Range(0, audShoot.Length)], audShootVol);

        // create a Raycasthit to pass into physics raycast
        gunList[selectedGun].ammoCurrent--;
        GameManager.Instance.ammoCurr.text = gunList[selectedGun].ammoCurrent.ToString("F0");
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
            else
            {
                Instantiate(gunList[selectedGun].hitEffect, hit.point, Quaternion.identity);
            }
        }
        // create a timer that will last for the time passed in by shootRate
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    IEnumerator disableLeftandRight()
    {
        setHorizontalInputActivity(false);
        yield return new WaitForSeconds(timeToTurnOffHorizontalMovement);
        setHorizontalInputActivity(true);
    }

    public void setHorizontalInputActivity(bool activity)
    {
        HorizontalInputEnabled = activity;
    }

    public void takeDamage(float amount)
    {
        aud.PlayOneShot(audHurt[Random.Range(0, audHurt.Length)], audHurtVol);
        currentHP -= amount;
        UpdatePlayerUI();
        StartCoroutine(FlashDamage());

        if (currentHP <= 0) 
        {
            GameManager.Instance.LoseGame();
            GameManager.Instance.holdController.drop();
            aud.PlayOneShot(audDeath, audDeathVol);
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
            wallJumpforce = transform.up * wallJumpVertSpeed + wallNormal * wallJumpSpeed;
           
            wallJumpTimes++;
            playerVel = wallJumpforce;
        }
        else if (wallLeft)
        {
            wallNormal = leftWallHit.normal;
            wallJumpforce = transform.up * wallJumpVertSpeed + wallNormal * wallJumpSpeed;
           
            wallJumpTimes++;
            playerVel = wallJumpforce;
        }
        StartCoroutine(disableLeftandRight());
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

    private void Dash()
    {
        Vector3 dashDirection = Camera.main.transform.forward.normalized;

        playerVel = dashDirection * dashSpeed;
        StartCoroutine(DashLengthWhileGrounded());
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashLengthWhileGrounded()
    {
        Debug.Log("dash on ground");
        isDashing = true;
        yield return new WaitForSeconds(dashDurationWhileGrounded);
        isDashing = false;
    }

    IEnumerator DashCooldown() 
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void WallRunStart()
    {
        runSpeed += wallRunSpeed;

        gravity = wallRunGravity;
        StartCoroutine(WallRunDuration());
    }

    private void WallRunStop()
    {
        runSpeed = speedOriginal;
        isWallRunning = false;
        gravity = gravityOriginal;
    }

    IEnumerator WallRunDuration()
    {
        isWallRunning = true;
        yield return new WaitForSeconds(wallRunDuration);
        WallRunStop();
    }

    private bool CheckForPlatform()
    {
        RaycastHit hit;
        // create a raycast and check to see if it hit something        
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1.15f, movingPlatformMask))
        {
            platform = hit.collider.gameObject;
            platformSpeed = platform.GetComponentInParent<MovingPlatformController>().speed;
            controller.enabled = false;
            transform.parent = platform.transform;
            return true;
        }
        else
        {
            platform = null;
            controller.enabled = true;
            transform.parent = null;
            return false;
        }
    }

    public void SpawnPlayer()
    {
        currentHP = maxHP;
        UpdatePlayerUI();

        controller.enabled = false;
        transform.position = GameManager.Instance.playerSpawnPos.transform.position;
        controller.enabled = true;
    }

    public void GetGunStats(GunStats gun)
    {
        gunList.Add(gun);
        selectedGun = gunList.Count - 1;

        shootDamage = gun.damage;
        shootRate = gun.rateOfFire;
        shootDist = gun.range;

        gunModel.GetComponent<MeshFilter>().sharedMesh = gun.gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gun.gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        GameManager.Instance.ammoMax.text = gunList[selectedGun].ammoMax.ToString("F0");
        GameManager.Instance.ammoCurr.text = gunList[selectedGun].ammoCurrent.ToString("F0");
    }

    private void SelectGun()
    {
        if ((Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKey(KeyCode.Alpha1)) && selectedGun < gunList.Count - 1)
        {
            selectedGun++;
            ChangeGun();
        }
        else if ((Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKey(KeyCode.Alpha2)) && selectedGun > 0)
        {
            selectedGun--;
            ChangeGun();
        }
    }

    private void ChangeGun()
    {
        shootDamage = gunList[selectedGun].damage;
        shootRate = gunList[selectedGun].rateOfFire;
        shootDist = gunList[selectedGun].range;

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunList[selectedGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunList[selectedGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        GameManager.Instance.ammoMax.text = gunList[selectedGun].ammoMax.ToString("F0");
        GameManager.Instance.ammoCurr.text = gunList[selectedGun].ammoCurrent.ToString("F0");
    }

    public Vector3 GetVelocity()
    {
        return controller.velocity;
    }
}
