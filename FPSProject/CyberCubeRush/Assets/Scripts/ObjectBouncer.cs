using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBouncer : MonoBehaviour
{
    [SerializeField] float bounceHeight = 1f;
    [SerializeField] float bounceSpeed = 1f;
    [SerializeField] float rotationSpeed = 50f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Bounce
        Vector3 bounceOffset = new Vector3(0f, Mathf.Sin(Time.time * bounceSpeed) * bounceHeight, 0f);
        transform.position = startPos + bounceOffset;

        // Rotate
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
