using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] int damage;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {            
            dmg.takeDamage(damage);
        }
    }
}
