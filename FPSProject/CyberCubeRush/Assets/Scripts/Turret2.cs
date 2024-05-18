using System.Collections;
using UnityEngine;

public class Turret2 : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Transform shootPosition;
    [SerializeField] Transform swivle;
    [SerializeField] GameObject bullet;

    [SerializeField] float intervalBetweenShots;
    [SerializeField] float trackingResponsiveness;
    
    private Vector3 lookPos;
    private GameObject player;
    private Vector3 pointDir;
    private Vector3 rotationGoal;
    private bool playerInRange = false;
    private bool isShooting = false;

    void Start()
    {
        player = GameManager.Instance.player;

        NewLookPos();
    }

    void Update()
    {
        if (playerInRange)
        {
            AimAtPoint(player.transform.position);

            if (Vector3.Dot(swivle.transform.forward, (player.transform.position - swivle.transform.position).normalized) > .7f)
            {
                if (!isShooting)
                    StartCoroutine(Shoot());
            }
        }     
        else
        {            
            AimAtPoint(lookPos);

            if (Vector3.Dot(swivle.transform.forward, (lookPos - swivle.transform.position).normalized) > .999f)
            {
                NewLookPos();
            }
        }
    }

    private void NewLookPos()
    {
        lookPos = transform.position;
        lookPos.x += Random.Range(-10f, 10f);
        lookPos.y += Random.Range(-10f, 10f);
        lookPos.z += Random.Range(-10f, 10f);
    }

    private void AimAtPoint(Vector3 point)
    {
        pointDir = point - shootPosition.transform.position;
        Debug.DrawRay(shootPosition.transform.position, pointDir, Color.red);
        Debug.DrawRay(shootPosition.transform.position, shootPosition.transform.forward * pointDir.magnitude, Color.green);

        // y rotation
        rotationGoal = Vector3.RotateTowards(transform.forward, pointDir, trackingResponsiveness * Time.deltaTime, 0.0f);
        Vector3 yRot = rotationGoal;
        //yRot.y = 0;
        transform.rotation = Quaternion.LookRotation(yRot);

        // xRotation
        Quaternion newLook = Quaternion.LookRotation(point - swivle.transform.position);
        Vector3 trimmedRot = Quaternion.Slerp(swivle.transform.rotation, newLook, trackingResponsiveness * Time.deltaTime).eulerAngles;
        trimmedRot.y = 0;
        trimmedRot.z = 0;

        swivle.localEulerAngles = trimmedRot;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void ShootBullet()
    {
        //aud.PlayOneShot(audShoot[Random.Range(0, audShoot.Length)], audShootVol);
        Instantiate(bullet, shootPosition.position, swivle.transform.rotation);
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        animator.SetBool("Shoot", true);

        yield return new WaitForSeconds(intervalBetweenShots);

        isShooting = false;
        animator.SetBool("Shoot", false);
    }
}
