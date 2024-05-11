using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] GameObject platformD;
    [SerializeField] Material making;
    public float currentTime = 0;
    bool isDisappearing;
    bool isAppearing;
    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        IDamage dmg = other.GetComponent<playerController>();

        if (dmg != null)
        {


            Debug.Log("Standing on Disapearing platform");
            if (making.color.a <= 0)
            {
                platformD.SetActive(false);
            }
            else
            {
                StartCoroutine(Vanishing());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        StartCoroutine(Appearing());
    }
    IEnumerator Vanishing()
    {
        isDisappearing = true;
        isAppearing = false;
        yield return new WaitForSeconds(2);
        isDisappearing = false;
    }
    IEnumerator Appearing()
    {
        isAppearing = true;
        yield return new WaitForSeconds(2);
        if (making.color.a >= 255 && isAppearing)
        {
            isAppearing = false;
        }
        else
        { 
            
            StartCoroutine(Appearing()); 
        }
    }
}
