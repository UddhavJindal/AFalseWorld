using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UMovingPlatforms : MonoBehaviour
{
    [SerializeField] Transform[] points;

    [SerializeField] float platformSpeed;

    int counter;

    private void Update()
    {
        if(Vector3.Distance(transform.position, points[counter].position) <= 0)
        {
            counter++;
            if(counter == points.Length)
            {
                counter = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, points[counter].position, platformSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
