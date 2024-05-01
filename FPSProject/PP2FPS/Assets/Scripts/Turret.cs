using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Turret : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;
    [SerializeField] Transform[] shootPositions;
    [SerializeField] Transform swivle;
    [SerializeField] GameObject bullet;
    [SerializeField] Animator animator;


    [SerializeField] int health;
    [SerializeField] float shootRate;
    [SerializeField] float playerTrackingSpeed;
    [SerializeField] int viewCone;
    [SerializeField] int roamWait;

    private int shootingPosIndex;
    bool isShooting;
    bool inRange;
    bool destinationChosen;
    float angleToPlayer;
    Vector3 playerDir;

    

    // Update is called once per frame
    void Update()
    {
        playerDir = GameManager.Instance.player.transform.position - swivle.transform.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, playerDir.y + 1, playerDir.z), transform.forward);
        Debug.Log(angleToPlayer);
        Debug.DrawRay(swivle.transform.position, playerDir, Color.red);
        if (inRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(swivle.transform.position, playerDir, out hit))
            {
                if (hit.collider.CompareTag("Player") && angleToPlayer <= viewCone)
                {
                    faceTarget();
                    if (!isShooting)
                    {
                        StartCoroutine(shoot());
                    }
                }
                else
                {
                    StartCoroutine(roam());
                }
                
            }
        }
        else
        {
            StartCoroutine(roam());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    void faceTarget()
    {        
        Quaternion rotate = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, playerTrackingSpeed / 10); // adjust x,z rot
        rotate = Quaternion.LookRotation(playerDir);        
        swivle.transform.rotation = Quaternion.RotateTowards(swivle.transform.rotation, rotate, playerTrackingSpeed / 10); // adjust y rot
    }

    IEnumerator roam()
    {
        if (!destinationChosen )
        {
            destinationChosen = true;

            Quaternion roamRotate = Quaternion.Euler(0, Random.Range(-360, 360), 0); // Random rotation around Y-axis
            //Quaternion swivelRotate = Quaternion.Euler(Random.Range(-60, 60), Random.Range(-60, 60), 0); // Random rotation for swivel

            float elapsedTime = 0f;
            while (elapsedTime < roamWait)
            {
                // Rotate the turret gradually
                transform.rotation = Quaternion.RotateTowards(transform.rotation, roamRotate, playerTrackingSpeed / 10);
                //swivle.rotation = Quaternion.Lerp(swivle.rotation, swivelRotate, Time.deltaTime * playerTrackingSpeed);
                
                // only start waiting once within a threshold value from look dist
                // Dot product returns -1 to 1, representing the vectors orientation to one another
                // 1 means they are the same
                if (Quaternion.Dot(transform.rotation.normalized, roamRotate.normalized) > .8f) 
                    elapsedTime += Time.deltaTime;              

                Debug.Log($"elapsed time: {elapsedTime}, transform.rotation: {transform.rotation.normalized}, roamRotate: {roamRotate.normalized}, Dot: {Quaternion.Dot(transform.rotation.normalized, roamRotate.normalized)}");
                yield return null;
            }

            destinationChosen = false;
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(redFlash());

        if (health <= 0)
        {
            GameManager.Instance.UpdateEnemyCounter(-1);
            Destroy(gameObject);
        }
    }

    IEnumerator redFlash()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = Color.white;
    }

    IEnumerator shoot()
    {
        isShooting = true;
        animator.SetBool("Shoot", true);
        
        yield return new WaitForSeconds(shootRate);

        isShooting = false;
        animator.SetBool("Shoot", false);
    }

    public void ShootBullet()
    {
        if (shootPositions.Length > 1 && shootingPosIndex + 1 != shootPositions.Length)
        {
            shootingPosIndex++;
        }
        else
        {
            shootingPosIndex = 0;
        }
        Instantiate(bullet, shootPositions[shootingPosIndex].position, swivle.transform.rotation);
    }
}
