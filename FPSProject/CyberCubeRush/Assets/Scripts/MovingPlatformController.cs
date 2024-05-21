using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField] Transform platform;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] public float speed;

    bool movingToEndPoint = true;

    // Start is called before the first frame update
    void Start()
    {
        //platform.position = startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetLocation = whereToMove();
        float dfromst = Vector3.Distance(platform.position, startPoint.position);
        float dfromend = Vector3.Distance(platform.position, endPoint.position);
        //float movingd = Vector3.Distance(platform.position, targetLocation)/2;
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
        
        if ((targetLocation - platform.position).magnitude <= 0.3f) 
        { 
            movingToEndPoint = !movingToEndPoint;
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
