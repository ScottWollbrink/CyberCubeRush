using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int slowing;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        playerController victim = other.GetComponent<playerController>();

        if (victim != null)
        {
            victim.runSpeed -= slowing; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        playerController victim = other.GetComponent<playerController>();

        if (victim != null)
        {
            victim.runSpeed += slowing;
        }
    }
}
