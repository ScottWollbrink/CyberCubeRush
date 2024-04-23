using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform platform;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float speed;

    bool movingToEndPoint = true;

    // Start is called before the first frame update
    void Start()
    {
        platform.position = startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetLocation = whereToMove();

        platform.position = Vector3.Lerp(platform.position, targetLocation, speed * Time.deltaTime);

        if ((targetLocation - platform.position).magnitude <= 0.1f) 
        { 
            movingToEndPoint = !movingToEndPoint;
        }
    }

    Vector3 whereToMove()
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
