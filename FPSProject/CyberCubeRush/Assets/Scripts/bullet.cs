using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int destroyTime;

    [SerializeField] Rigidbody rb;

    bool hitHappen;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * speed;
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.tag != "Cube")
        {
            return;
        }
        IDamage dmg = other.GetComponent<IDamage>();

        if(dmg != null && !hitHappen)
        {
            dmg.takeDamage(damage);
            hitHappen = true;
        }
        Destroy(gameObject);
    }
}
