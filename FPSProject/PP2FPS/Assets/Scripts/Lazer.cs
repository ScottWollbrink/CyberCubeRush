using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Lazar : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] int lazerDamage;
    [SerializeField] int lazerRange;
    [SerializeField] float damageWait;

    RaycastHit hit;
    bool candamage;

    // Start is called before the first frame update
    void Start()
    {
        candamage = true;
        lineRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.forward * lazerRange);

        if (Physics.Raycast(transform.position, transform.forward, out hit, lazerRange))
        {
            IDamage dmg = hit.collider.GetComponent<IDamage>();
            if (hit.transform != transform && dmg != null && candamage)
            {
                // pass damage to dmg take damage method
                dmg.takeDamage(lazerDamage);
                StartCoroutine(damageTimer());
            }
            lineRenderer.SetPosition(1, hit.point);
        }
    }

    IEnumerator damageTimer()
    {
        candamage = false;
        yield return new WaitForSeconds(damageWait);
        candamage = true;
    }
}
