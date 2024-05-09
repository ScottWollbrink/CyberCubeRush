using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] AudioSource aud;
    [SerializeField] Transform spike;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] int damage;
    [SerializeField] public float outspeed;
    [SerializeField] public float inspeed;
    [SerializeField] public float goingspeed;
    [SerializeField] public float waitTime;
    [SerializeField] AudioClip audOut;
    [SerializeField, Range(0, 1f)] float audoutVol;
    [SerializeField] AudioClip audIn;
    [SerializeField, Range(0, 1f)] float audInVol;
    public bool movingToEndPoint = true;

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
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetLocation = whereToMove();

        spike.position = Vector3.MoveTowards(spike.position, targetLocation, goingspeed * Time.deltaTime);

        if ((targetLocation - spike.position).magnitude <= 0.3f)
        {
            goingspeed = 0;
            StartCoroutine(waiting());
            aud.Stop();
            //targetLocation = whereToMove();
        }
    }
    IEnumerator waiting()
    {
        movingToEndPoint = !movingToEndPoint;
        yield return new WaitForSeconds(waitTime);
        
        if (movingToEndPoint)
        {
            goingspeed = inspeed;
            aud.PlayOneShot(audIn, audInVol);
            
        }
        else
        {
            goingspeed = outspeed;
            aud.PlayOneShot(audOut, audoutVol);

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {
            if (movingToEndPoint)
                dmg.takeDamage(damage);
            else
                dmg.takeDamage(damage*2);
        }
    }
}
