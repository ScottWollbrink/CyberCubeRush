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
    float posX, posY, posZ, angle = 0f;
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
