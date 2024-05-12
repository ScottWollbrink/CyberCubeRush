using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    [SerializeField] float damage;
    bool isDraining;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null && !isDraining)
        {
            //dmg.takeDamage(damage);
            StartCoroutine(Draining(dmg));
        }
    }
    IEnumerator Draining(IDamage dmg)
    {
        isDraining = true;
        dmg.takeDamage(damage);
        yield return new WaitForSeconds(1);
        isDraining = false;
    }
}
