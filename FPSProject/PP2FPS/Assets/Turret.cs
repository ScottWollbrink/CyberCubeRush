using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Turret : MonoBehaviour
{
    [SerializeField] Renderer model;
    [SerializeField] Transform[] shootPositions;
    [SerializeField] Transform swivle;
    [SerializeField] GameObject bullet;
    [SerializeField] Animator animator;


    [SerializeField] int health;
    [SerializeField] float shootRate;
    [SerializeField] float playerTrackingSpeed;

    private int shootingPosIndex;
    bool isShooting;
    bool inRange;
    Vector3 playerDir;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UpdateEnemyCounter(1);
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = GameManager.Instance.player.transform.position - swivle.transform.position;
        Debug.DrawRay(swivle.transform.position, playerDir, Color.red);
        if (inRange)
        {

            RaycastHit hit;
            if (Physics.Raycast(swivle.transform.position, playerDir, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    faceTarget();
                    if (!isShooting)
                    {
                        StartCoroutine(shoot());
                    }
                }
            }
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
        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Time.deltaTime * playerTrackingSpeed); // adjust x,z rot
        rotate = Quaternion.LookRotation(playerDir);        
        swivle.transform.rotation = Quaternion.Lerp(swivle.transform.rotation, rotate, Time.deltaTime * playerTrackingSpeed * .5f); // adjust y rot
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
