using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDamaging : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float damage;
    [SerializeField] int damMult;
    [SerializeField] int damdiv;
    public bool isDaming;
    
    
    public IEnumerator DamingEnter(IDamage dmg)
    {
        if (!isDaming)
        {
            isDaming = true;
            dmg.takeDamage(damage * damMult);
            yield return new WaitForSeconds(1);
            isDaming = false;
        }
        else
            yield return null;
    }
    public IEnumerator DamingStay(IDamage dmg)
    {
        if (!isDaming)
        {
            isDaming = true;
            dmg.takeDamage(damage * damMult / damdiv);
            yield return new WaitForSeconds(1);
            isDaming = false;
        }
    }
}
