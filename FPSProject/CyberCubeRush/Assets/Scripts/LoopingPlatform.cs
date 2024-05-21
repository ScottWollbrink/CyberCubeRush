using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LoopingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] Transform platform;
    [SerializeField] public float speed;
    int nextPoint;
    [Header("Circular moving")]
    [SerializeField] Transform center;
    [SerializeField] float rotationRadius;
    //float posX, posY, posZ, angle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        nextPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {//circular looping mechanics to be implemented later
        //posX = center.position.x + Mathf.Cos(angle) * rotationRadius;
        //posY = center.position.y + Mathf.Sin(angle) * rotationRadius;
        //posZ = center.position.z + Mathf.Tan(angle) * rotationRadius;
        Vector3 targetLocation = points[nextPoint].position;
        float dfromst = Vector3.Distance(platform.position, points[nextPoint].position);
        float dfromend = Vector3.Distance(platform.position, points[(nextPoint-1 + points.Length) %points.Length].position);
        //float movingd = Vector3.Distance(platform.position, targetLocation) / 2;
        if (dfromst > dfromend)
        {
            dfromst /= 2;
            dfromend *= 2;
        }
        else if (dfromst < dfromend)
        {
            dfromend /= 2;
            dfromst *= 2;
        }
        speed = (dfromst + dfromend) / 4;
        platform.position = Vector3.MoveTowards(platform.position, targetLocation, speed * Time.deltaTime);
        //angle = angle + Time.deltaTime * speed;
//        if (angle >= 360)
 //           angle -= 360;
        if ((targetLocation - platform.position).magnitude <= 0.3f)
        {
            nextPoint++;
            if(nextPoint >= points.Length)
            {
                nextPoint = 0;
            }
        }
    }

    
}
