using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMovement : MonoBehaviour
{
    [SerializeField] Transform[] points;

    float MovementSpeed;

    [SerializeField] float minSpeed = 5;
    [SerializeField] float maxSpeed = 10;

    //counter
    int counter;

    private void Start()
    {
        counter = Random.Range(0, points.Length);
        MovementSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, points[counter].position) <= 0)
        {
            counter = Random.Range(0, points.Length);
            MovementSpeed = Random.Range(minSpeed, maxSpeed);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, points[counter].position, MovementSpeed * Time.deltaTime);
    }
}
