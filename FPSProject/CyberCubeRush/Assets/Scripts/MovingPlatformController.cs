using System.Collections;
using System.Collections.Generic;
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
