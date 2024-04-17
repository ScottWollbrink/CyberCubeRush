using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAi : MonoBehaviour, IDamage
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;

    [SerializeField] int health;
    [SerializeField] float shootRate;
    [SerializeField] int rotateSpeed;

    bool isShooting;
    bool inRange;
    Vector3 playerDir;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            playerDir = GameManager.Instance.player.transform.position - transform.position;
            agent.SetDestination(GameManager.Instance.player.transform.position);

            if (!isShooting)
            {
                StartCoroutine(shoot());
            }

            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                faceTarget();
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
        Quaternion rotate = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Time.deltaTime * rotateSpeed);
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(redFlash());
        agent.SetDestination(GameManager.Instance.player.transform.position);

        agent.SetDestination(GameManager.Instance.player.transform.position);

        if (health <= 0)
        {
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
        Instantiate(bullet, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}