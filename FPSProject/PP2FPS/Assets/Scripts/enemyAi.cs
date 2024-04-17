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

    bool isShooting;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(GameManager.Instance.player.transform.position);

        if (!isShooting )
        {
            StartCoroutine(shoot());
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(redFlash());

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