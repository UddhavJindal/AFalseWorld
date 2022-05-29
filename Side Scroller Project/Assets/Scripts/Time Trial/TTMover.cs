using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTMover : MonoBehaviour
{
    [SerializeField] Transform Platform;

    [SerializeField] Transform[] Points;

    [SerializeField] float platformSpeed;

    int counter = 0;
    int noOfPoints;

    [SerializeField] bool isForward;

    private void Start()
    {
        noOfPoints = Points.Length;
        if(isForward)
        {
            counter = 0;
        }
        else
        {
            counter = noOfPoints - 1;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(Platform.position, Points[counter].position) <= 0)
        {
            counter++;
            if(counter >= noOfPoints)
            {
                counter = 0;
            }
        }
        Platform.position = Vector3.MoveTowards(Platform.position, Points[counter].position, platformSpeed * Time.deltaTime);

    }
}
