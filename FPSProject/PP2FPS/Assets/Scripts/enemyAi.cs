using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAi : MonoBehaviour, IDamage
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;
    [SerializeField] GameObject bullet;

    [SerializeField] int health;
    [SerializeField] float shootRate;
    [SerializeField] int rotateSpeed;
    [SerializeField] int viewCone;
    [SerializeField] int roamDistance;
    [SerializeField] int roamPause;

    bool isShooting;
    bool inRange;
    Vector3 playerDir;
    Vector3 startingPos;
    float angleToPlayer;
    bool destinationChosen;

    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = GameManager.Instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.position);
        Debug.DrawRay(headPos.position, playerDir);
        if (inRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(headPos.position, playerDir, out hit))
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

    bool canSeePlayer()
    {
        
        return false;
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
        Quaternion rotate = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Time.deltaTime * rotateSpeed);
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(redFlash());
        agent.SetDestination(GameManager.Instance.player.transform.position);

        if (health <= 0)
        {
            GameManager.Instance.UpdateEnemyCounter(-1);
            Destroy(gameObject);
        }
    }

    IEnumerator roam()
    {
        if (!destinationChosen)
        {
            destinationChosen = true;

            Quaternion roamRotate = Quaternion.Euler(0, Random.Range(-360, 360), 0); // Random rotation around Y-axis
            //Quaternion swivelRotate = Quaternion.Euler(Random.Range(-60, 60), Random.Range(-60, 60), 0); // Random rotation for swivel

            float elapsedTime = 0f;
            while (elapsedTime < roamPause)
            {
                // Rotate the turret gradually
                transform.rotation = Quaternion.RotateTowards(transform.rotation, roamRotate, Time.deltaTime * rotateSpeed);
                //swivle.rotation = Quaternion.Lerp(swivle.rotation, swivelRotate, Time.deltaTime * playerTrackingSpeed);

                // only start waiting once within a threshold value from look dist
                // Dot product returns -1 to 1, representing the vectors orientation to one another
                // 1 means they are the same
                if (Quaternion.Dot(transform.rotation.normalized, roamRotate.normalized) > .8f)
                {
                    elapsedTime += Time.deltaTime;
                }
                
                yield return null;
            }
            destinationChosen = false;
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
        Instantiate(bullet, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}