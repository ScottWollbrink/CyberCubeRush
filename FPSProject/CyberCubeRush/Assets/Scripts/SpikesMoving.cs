using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] AudioSource aud;
    [SerializeField] Transform spike;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] SpikesDamaging parent;
    
    [SerializeField] public float outspeed;
    [SerializeField] public float inspeed;
    [SerializeField] public float goingspeed;
    [SerializeField] public float waitTime;
    [SerializeField] AudioClip audOut;
    [SerializeField] AudioClip audResting;
    [SerializeField] AudioClip audIn;
    [SerializeField, Range(0, 1f)] float vol;
    public bool movingToEndPoint = true;
    public bool willMove;

    // Start is called before the first frame update
    void Start()
    {
        spike.position = startPoint.position;
        if (movingToEndPoint)
        {
            goingspeed = inspeed;

        }
        else
        {
            goingspeed = outspeed;


        }
        willMove = parent.GetComponent<SpikesDamaging>().isMovingChilds;
        if (!willMove)
        { 
            aud.clip = audResting;
            aud.loop = true;
            //aud.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {

            StartCoroutine(parent.DamingEnter(dmg));
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null && !parent.isDaming)
        {
            //dmg.takeDamage(damage);
            StartCoroutine(parent.DamingStay(dmg));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (willMove)
        { 
            Vector3 targetLocation = whereToMove();

            spike.position = Vector3.MoveTowards(spike.position, targetLocation, goingspeed * Time.deltaTime);

            if ((targetLocation - spike.position).magnitude <= 0.3f)
            {
                goingspeed = 0;

                StartCoroutine(waiting());

            //targetLocation = whereToMove();
            }
        }
    }
    IEnumerator sparking() 
    {
        yield return null;
    }
    IEnumerator waiting()
    {
        movingToEndPoint = !movingToEndPoint;
        aud.clip = audResting;
        aud.Stop();
        if (movingToEndPoint)
            aud.PlayOneShot(audResting, vol);

        
        yield return new WaitForSeconds(waitTime);
        aud.Stop();
        if (movingToEndPoint)
        {
            goingspeed = inspeed;
            aud.PlayOneShot(audIn, vol);
            
        }
        else
        {
            goingspeed = outspeed;
            aud.PlayOneShot(audOut, vol);

        }
    }
    public Vector3 whereToMove()
    {
        if (movingToEndPoint)
        {
            
            return endPoint.position;
        }
        else
        {
            
            return startPoint.position;

        }
    }
    
}
