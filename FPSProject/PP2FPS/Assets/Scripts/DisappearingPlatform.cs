using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] GameObject platformD;
    [SerializeField] Renderer making;
    public float currentTime = 0;
    bool isDisappearing;
    bool isAppearing;
    public float alpha;//debugging field
    private void OnTriggerStay(Collider other)
    {
        alpha = making.material.color.a;
        if (other.isTrigger)
        {
            return;
        }
        IDamage dmg = other.GetComponent<playerController>();

        if (dmg != null && !isDisappearing)
        {
            StartCoroutine(Vanishing());
        }
        if (making.material.color.a <= 0 && isDisappearing)
        { 
            platformD.SetActive(false);
            isDisappearing = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
    IEnumerator Vanishing()
    {
        isDisappearing = true;
        
        yield return new WaitForSeconds(1);
        making.material.color = new Color(making.material.color.r, making.material.color.g, making.material.color.b, making.material.color.a - .25f);
        isDisappearing = false;
        if(making.material.color.a <= 0) 
            StartCoroutine(Appearing());
    }
    IEnumerator Appearing()//method to be futher to incorporate fading in done if returning the platform is desired
    {
        alpha = making.material.color.a;
        yield return new WaitForSeconds(5);
        platformD.SetActive(true);
        making.material.color = new Color(making.material.color.r, making.material.color.g, making.material.color.b, 1);
        alpha = making.material.color.a;
    }
}
